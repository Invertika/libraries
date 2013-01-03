using System;
using System.Net.Sockets;
using ISL.Server.Network;
using ISL.Server.Common;
using System.IO;
using ISL.Server.Utilities;
using System.Text;
using System.Collections.Generic;

namespace ISL.Server
{
    public class WebSocketReader
    {
        NetworkStream baseStream;

        public WebSocketReader(NetworkStream stream)
        {
            baseStream=stream;
        }

        /// <summary>
        /// Reads the websocket handshake.
        /// Der Handshake ist dabei normaler Text und kein Websocket Paket
        /// </summary>
        /// <returns>
        /// The websocket handshake.
        /// </returns>
        public string ReadWebsocketHandshake()
        {
            byte[] handshake=new byte[1024];
            baseStream.Read(handshake, 0, handshake.Length);

            UTF8Encoding encoding=new UTF8Encoding();
            return encoding.GetString(handshake);
        }

        public MessageIn ReadMessage(out bool websocketClosed)
        {
            byte[] webSocketPacket=new byte[]{};
            websocketClosed=false;

            while(webSocketPacket.Length==0)
            {
                webSocketPacket=ReadWebsocketPackage(out websocketClosed);

                if(webSocketPacket.Length==0)
                {
                    Logger.Write(LogLevel.Warning, "Recieve empty WebSocket package.");
                }
            }

            return new MessageIn(webSocketPacket);
        }

        byte[] ReadWebsocketPackage(out bool websocketClosed)
        {
            byte[] buffer=new byte[2];
            baseStream.ReadSecure(buffer, 0, 2);

            bool fin=(buffer[0]&0x80)==0x80;

            bool rsv1=(buffer[0]&0x40)==0x40;
            bool rsv2=(buffer[0]&0x20)==0x20;
            bool rsv3=(buffer[0]&0x10)==0x10;

            int opCode=((buffer[0]&0x8)|(buffer[0]&0x4)|(buffer[0]&0x2)|(buffer[0]&0x1));

            bool mask=(buffer[1]&0x80)==0x80;

            byte payload=(byte)((buffer[1]&0x40)|(buffer[1]&0x20)|(buffer[1]&0x10)|(buffer[1]&0x8)|(buffer[1]&0x4)|(buffer[1]&0x2)|(buffer[1]&0x1));
            ulong length=0;

            switch(payload)
            {
                case 126:
                    {
                        buffer=new byte[2];
                        baseStream.ReadSecure(buffer, 0, 2);
                        byte[] bytesUShort=buffer;
                        if(bytesUShort!=null)
                        {
                            Array.Reverse(bytesUShort);
                            length=BitConverter.ToUInt16(bytesUShort, 0);
                        }
                        break;
                    }
                case 127:
                    {
                        buffer=new byte[8];
                        baseStream.ReadSecure(buffer, 0, 8);
                        byte[] bytesULong=buffer;
                        if(bytesULong!=null)
                        {
                            Array.Reverse(bytesULong);
                            length=BitConverter.ToUInt16(bytesULong, 0);
                        }
                        break;
                    }
                default:
                    {
                        length=payload;
                        break;
                    }
            }

            byte[] maskKeys=null;
            if(mask)
            {
                buffer=new byte[4];
                baseStream.ReadSecure(buffer, 0, 4);
                maskKeys=buffer;
            }

            buffer=new byte[length];
            baseStream.ReadSecure(buffer, 0, (int)length);
            byte[] data=buffer;

            if(mask)
            {
                for(int i=0;i<data.Length;++i)
                {
                    data[i]=(byte)(data[i]^maskKeys[i%4]);
                }
            }

            ushort closeCode=0;
            if(opCode==(int)WebsocketOpCode.Close&&data.Length==2)
            {
                byte[] dataCloned=(byte[])data.Clone();
                Array.Reverse(dataCloned);
                closeCode=BitConverter.ToUInt16(dataCloned, 0);
            }

            if(closeCode!=0)
            {
                websocketClosed=true;
            }
            else
            {
                websocketClosed=false;
            }

            return data;
        }
    }
}

