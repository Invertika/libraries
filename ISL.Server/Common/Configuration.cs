//
//  Configuration.cs
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
using CSCL;
using System.Xml;

namespace ISL.Server.Common
{
	public static class Configuration
	{
		public static string Filename { get; private set; }

		public const int DEFAULT_SERVER_PORT=9601;

		public const string DEFAULT_CONFIG_FILE="manaserv.xml";

		static XmlData xmlfile;
		static List<XmlNode> nodes;

		public static void Init(string filename)
		{
			if(filename==null||filename=="")
			{
				Filename=DEFAULT_CONFIG_FILE;
			}
			else
			{
				Filename=filename;
			}

			if(!FileSystem.ExistsFile(Filename)) throw new Exception();

			xmlfile=new XmlData(Filename);
			nodes=xmlfile.GetElements("configuration.option");
		}

		public static void deinitialize()
		{
		}

		public static string getValue(string key, string deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					return node.Attributes["value"].Value;
				}
			}

			return deflt;
		}

		public static int getValue(string key, int deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					return Convert.ToInt32(node.Attributes["value"].Value);
				}
			}

			return deflt;
		}

		public static bool getBoolValue(string key, bool deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					if(node.Attributes["value"].Value=="0") return false;
					else if (node.Attributes["value"].Value=="1") return true;
					else return Convert.ToBoolean(node.Attributes["value"].Value);
				}
			}

			return deflt;
		}
	}
}
