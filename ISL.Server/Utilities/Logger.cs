//
//  Logger.cs
//
//  This file is part of Invertika (http://invertika.org)
// 
//  Based on The Mana Server (http://manasource.org)
//  Copyright (C) 2004-2012  The Mana World Development Team 
//
//  Author:
//       seeseekey <seeseekey@googlemail.com>
// 
//  Copyright (c) 2011, 2012 by Invertika Development Team
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ISL.Server.Utilities
{
	/// <summary>
	/// Enum für das Loglevel
	/// </summary>
	public enum LogMode
	{
		None,
		Internal,
		Debug,
		File
	}

	/// <summary>
	/// Der Loglevel
	/// </summary>
	public enum LogLevel
	{
		Fatal=0,
		Error,
		Warning,
		Debug,
		Information
	}

	/// <summary>
	/// Klasse für einen Logeintrag
	/// </summary>
	public class LogEntry
	{
		public DateTime Timecode { get; private set; }
		public LogLevel Mode { get; private set; }
		public string Message { get; private set; }

		public LogEntry(LogLevel mode, string message)
		{
			Timecode=DateTime.Now;
			Mode=mode;
			Message=message;
		}

		public override string ToString()
		{
			return String.Format("[{0:D2}:{1:D2}:{2:D2}] ({3}): {4}", Timecode.Hour, Timecode.Minute, Timecode.Second, Mode.ToString().ToUpper(), Message);
			//return String.Format("[{0:D4}.{1:D2}.{2:D2}]:[{3:D2}:{4:D2}:{5:D2}:{6:D3}] ({7}): {8}", Timecode.Year, Timecode.Month, Timecode.Day, Timecode.Hour, Timecode.Minute, Timecode.Second, Timecode.Millisecond, Mode.ToString().ToUpper(), Message);
			//return String.Format("[{0:D4}.{1:D2}.{2:D2}] . [{3:D2}:{4:D2}:{5:D2}:{6:D3}] . {7} . {8}", Timecode.Year, Timecode.Month, Timecode.Day, Timecode.Hour, Timecode.Minute, Timecode.Second, Timecode.Millisecond, Mode.ToString().ToUpper(), Message);
		}
	}

	/// <summary>
	/// Generische Logklasse
	/// </summary>
	public static class Logger //TODO Logmodes überarbeiten
	{
		public static List<LogEntry> Log { get; private set; }
		public static LogMode LogMode { get; private set; }
		public static string LogFile { get; private set; }

		public static event EventHandler OnNewLogEntry;

		public static void FireEventOnNewLogEntry(object obj, EventArgs args)
		{
			if(OnNewLogEntry!=null) OnNewLogEntry(obj, args);
		}

		//Private
		static StreamWriter logFileStream=null;

		static Logger()
		{
			Log=new List<LogEntry>();
			LogMode=LogMode.None;
		}

		public static void Init(string logFilename) //TODO Nutzer soll Logpfad bestimmen können
		{
			LogFile=logFilename;
		}

		/// <summary>
		/// Ändert den Logmodus
		/// </summary>
		/// <param name="logmode"></param>
		public static void ChangeLogMode(LogMode logmode)
		{
			if(LogMode==logmode) return;

			if(LogMode==LogMode.File)
			{
				logFileStream.Close();
			}

			if(logmode==LogMode.File)
			{
				logFileStream=new StreamWriter(LogFile);
			}

			LogMode=logmode;
		}

		/// <summary>
		/// Fügt einen neuen Eintrag zum Logsystem dazu
		/// </summary>
		/// <param name="message"></param>
		public static void Write(LogLevel logLevel, string message, params object[] arg)
		{
			if(LogMode==LogMode.None) return;

#if! DEBUG
			if(logLevel==LogLevel.Debug) return;
#endif

			message=String.Format(message, arg);
			LogEntry entry=new LogEntry(logLevel, message);

			switch(LogMode)
			{
				case LogMode.Internal:
					{
						Log.Add(entry);
						Console.WriteLine(entry);

						FireEventOnNewLogEntry(entry.ToString(), null);
						break;
					}
				case LogMode.Debug:
					{
						Log.Add(entry);
						Console.WriteLine(entry);
						Debug.WriteLine(entry.ToString());

						FireEventOnNewLogEntry(entry.ToString(), null);
						break;
					}
				case LogMode.File:
					{
						Log.Add(entry);
						Console.WriteLine(entry);
						Debug.WriteLine(entry.ToString());
						logFileStream.WriteLine(entry.ToString());
						logFileStream.Flush();

						FireEventOnNewLogEntry(entry.ToString(), null);
						break;
					}
				default:
					{
						break;
					}
			}
		}
	}
}
