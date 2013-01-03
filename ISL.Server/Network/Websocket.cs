using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading;
using ISL.Server.Utilities;
using System.Linq;
using ISL.Server.Network;
using ISL.Server.Common;
using System.IO;

namespace ISL.Server
{
    /// <summary>
    /// Klasse sorgt für den Websocket Handshake
    /// </summary>
    public static class Websocket
    {
        static string guid="258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        static SHA1 sha1=SHA1CryptoServiceProvider.Create();

        /// <summary>
        /// Raises the accept event.
        /// </summary>
        /// <param name='tcpClient'>
        /// Tcp client.
        /// </param>
        public static void OnAccept(TcpClient tcpClient)
        {
            byte[] buffer=new byte[1024];

            try
            {
                NetworkStream stream=tcpClient.GetStream();

                //Hier wird die Read Methode benutzt, da kein Websocketpaket eingelesen wird
                //sondern der Text dessen Länge unbekannt ist.
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
                        +"Sec-WebSocket-Accept: "+acceptKey+newLine+newLine;
                    //+ "Sec-WebSocket-Protocol: chat, superchat" + newLine
                    //+ "Sec-WebSocket-Version: 13" + newLine;

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

        /// <summary>
        /// Senden als Websocket Paket
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static byte[] GetWebsocketDataFrame(byte[] binary)
        {
            try
            {
                ulong headerLength=2;
                byte[] data=binary;

                bool mask=false;
                byte[] maskKeys=null;

                if(mask)
                {
                    headerLength+=4;
                    data=(byte[])data.Clone();

                    Random random=new Random(Environment.TickCount);
                    maskKeys=new byte[4];
                    for(int i=0;i<4;++i)
                    {
                        maskKeys[i]=(byte)random.Next(byte.MinValue, byte.MaxValue);
                    }

                    for(int i=0;i<data.Length;++i)
                    {
                        data[i]=(byte)(data[i]^maskKeys[i%4]);
                    }
                }

                byte payload;
                if(data.Length>=65536)
                {
                    headerLength+=8;
                    payload=127;
                }
                else if(data.Length>=126)
                {
                    headerLength+=2;
                    payload=126;
                }
                else
                {
                    payload=(byte)data.Length;
                }

                byte[] header=new byte[headerLength];

                header[0]=0x80|0x1;
                if(mask)
                {
                    header[1]=0x80;
                }
                header[1]=(byte)(header[1]|payload&0x40|payload&0x20|payload&0x10|payload&0x8|payload&0x4|payload&0x2|payload&0x1);

                if(payload==126)
                {
                    byte[] lengthBytes=BitConverter.GetBytes((ushort)data.Length).Reverse().ToArray();
                    header[2]=lengthBytes[0];
                    header[3]=lengthBytes[1];

                    if(mask)
                    {
                        for(int i=0;i<4;++i)
                        {
                            header[i+4]=maskKeys[i];
                        }
                    }
                }
                else if(payload==127)
                {
                    byte[] lengthBytes=BitConverter.GetBytes((ulong)data.Length).Reverse().ToArray();
                    for(int i=0;i<8;++i)
                    {
                        header[i+2]=lengthBytes[i];
                    }
                    if(mask)
                    {
                        for(int i=0;i<4;++i)
                        {
                            header[i+10]=maskKeys[i];
                        }
                    }
                }

                return header.Concat(data).ToArray();

            }
            catch(Exception ex)
            {
                Logger.Write(LogLevel.Error, "Websocket transport protocol Send exception: {0}", ex);
            }

            return null;
        }
    }
}
