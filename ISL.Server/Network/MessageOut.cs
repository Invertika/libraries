using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

		public MessageOut(int id)
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
