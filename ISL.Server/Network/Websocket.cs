using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;

namespace ISL.Server
{
	/// <summary>
	/// Klasse sorgt für den Websocket Handshake
	/// </summary>
	public static class Websocket
	{
		static string guid="258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
		static SHA1 sha1=SHA1CryptoServiceProvider.Create();

		public static void OnAccept(TcpClient tcpClient)
		{
			byte[] buffer=new byte[1024];

			try
			{
				NetworkStream stream=tcpClient.GetStream();
				int readed=stream.Read(buffer, 0, buffer.Length);

				string headerResponse=(System.Text.Encoding.UTF8.GetString(buffer)).Substring(0, readed);

				if(stream!=null)
				{
					//Handshaking and managing ClientSocket
					string key=headerResponse.Replace("ey:", "`")
							  .Split('`')[1]                     // dGhlIHNhbXBsZSBub25jZQ== \r\n .......
							  .Replace("\r", "").Split('\n')[0]  // dGhlIHNhbXBsZSBub25jZQ==
							  .Trim();

					// key should now equal dGhlIHNhbXBsZSBub25jZQ==
					string acceptKey=AcceptKey(ref key);

					string newLine="\r\n";

					string response="HTTP/1.1 101 Switching Protocols"+newLine
						 +"Upgrade: websocket"+newLine
						 +"Connection: Upgrade"+newLine
						 +"Sec-WebSocket-Accept: "+acceptKey+newLine+newLine
						//+ "Sec-WebSocket-Protocol: chat, superchat" + newLine
						//+ "Sec-WebSocket-Version: 13" + newLine
						 ;

					// which one should I use? none of them fires the onopen method
					byte[] responseArray=System.Text.Encoding.UTF8.GetBytes(response);
					stream.Write(responseArray, 0, responseArray.Length);
				}
			}
			catch(SocketException exception)
			{
				throw exception;
			}
			finally
			{
			}
		}

		static string AcceptKey(ref string key)
		{
			string longKey=key+guid;
			byte[] hashBytes=ComputeHash(longKey);
			return Convert.ToBase64String(hashBytes);
		}

		static byte[] ComputeHash(string str)
		{
			return sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
		}
	}
}
