//
//  NetworkStreamExtension.cs
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
using System.Net.Sockets;

namespace ISL.Server.Network
{
	public static class NetworkStreamExtension
	{
		/// <summary>
		/// Diese Extension sorgt dafür das der Stream solange gelesen wird
		/// bis die Anzahl size wirklich aus dem NetworkStream ausgelesen ist
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static byte[] Read(this NetworkStream stream, int size)
		{
			byte[] buffer=new byte[size];
			Read(stream, buffer, 0, size);
			return buffer;
		}

		static void Read(NetworkStream stream, byte[] buffer, int offset, int size)
		{
			while(size>0)
			{
				int read=stream.Read(buffer, offset, size);
				if(read==0) throw new Exception();
				size-=read;
				offset+=read;
			}
		}
	}
}
