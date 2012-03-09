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
