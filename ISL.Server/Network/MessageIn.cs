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
		 * Actual position in the packet. From 0 to packet->length. A value
		 * bigger than packet->length means EOP was reached when reading it.
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

		public string readString()
		{
			return reader.ReadString();
		}

		public override string ToString()
		{
			return mId.ToString();
		}
	}
}