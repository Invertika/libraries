//
//  MessageIn.cs
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
	public class MessageIn
	{
		BinaryReader reader;

		byte[] mData;            /**< Packet data */
		ushort mLength;       /**< Length of data in bytes */
		Protocol mId;           /**< The message ID. */

		/**
		 * Actual position in the packet. From 0 to packet.length. A value
		 * bigger than packet.length means EOP was reached when reading it.
		 */
		ushort mPos;

		//TODO Länge und Position noch benötigt?
		//TODO Typenkonvertierung? ushort etc.

		public MessageIn(byte[] data)
		{
			mData=data;
			mLength=(ushort)data.Length;
			mPos=0;

			reader=new BinaryReader(new MemoryStream(data));
			
			// Read the message ID
			mId=(Protocol)readInt16();
		}

		/**
 * Returns the message ID.
 */
		public Protocol getId()
		{
			return mId;
		}

		public byte readInt8()
		{
			return reader.ReadByte();
		}

		public short readInt16()
		{
			return reader.ReadInt16();
		}

		public int readInt32()
		{
			return reader.ReadInt32();
		}

		public double readDouble()
		{
			return reader.ReadDouble();
		}

		public string readString()
		{
			return reader.ReadString();
		}

		public override string ToString()
		{
			return mId.ToString();
		}

		public int getUnreadLength() 
		{
			return (int)(reader.BaseStream.Length-reader.BaseStream.Position);
		}
	}
}
