//
//  MessageOut.cs
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
using ISL.Server.Common;

namespace ISL.Server.Network
{
	public class MessageOut
	{
		MemoryStream data;
		BinaryWriter writer;

		public MessageOut()
		{
			data=new MemoryStream();
			writer=new BinaryWriter(data);
		}

		public MessageOut(Protocol id)
		{
			data=new MemoryStream();
			writer=new BinaryWriter(data);

			writer.Write((UInt16)id);
		}

		public void writeInt8(int value)
		{
			writer.Write((sbyte)value);
		}

		public void writeInt16(int value)
		{
			writer.Write((Int16)value);
		}

		public void writeInt32(int value)
		{
			writer.Write((Int32)value);
		}

		public void writeString(string value)
		{
			writer.Write(value);
		}

		 public byte[] getData() 
		 { 
			 return data.ToArray(); 
		 }

        /**
         * Returns the length of the data.
         */
		public uint getLength()  
		{ 
			return (uint)data.Length; 
		}
	}
}
