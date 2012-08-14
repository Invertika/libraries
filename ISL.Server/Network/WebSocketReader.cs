using System;
using System.Net.Sockets;
using ISL.Server.Network;
using ISL.Server.Common;
using System.IO;
using ISL.Server.Utilities;
using System.Text;

namespace ISL.Server
{
    public class WebSocketReader
    {
        NetworkStream baseStream;

        public WebSocketReader(NetworkStream stream)
        {
            baseStream = stream;
        }


        public MessageIn ReadMessage()
        {
            byte[] webSocketPacket = ReadWebsocketPackage();
            UTF8Encoding encoding = new UTF8Encoding();
            string message = encoding.GetString(webSocketPacket);
            return InterpretMessage(message);
        }

        /// <summary>
        /// Interprets the message.
        /// </summary>
        /// <returns>
        /// The message.
        /// </returns>
        /// <param name='message'>
        /// Message.
        /// </param>
        MessageIn InterpretMessage(string message)
        {
            try
            {
                //TODO Doppelpunkte in Stringnachrichten müssen maskiert werden
                Logger.Write(LogLevel.Debug, "Interpret message: {0}", message);
                string[] parts = message.Split(new char[] { ':' });

                int cmdValue = Int32.Parse(parts [0], System.Globalization.NumberStyles.HexNumber);
                Protocol command = (Protocol)cmdValue;

                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);

                //Bytepaket zusammenbauen
                writer.Write((UInt16)cmdValue);

                switch (command)
                {
                    case Protocol.PAMSG_LOGIN: //Login Kommando
                        {
                            writer.Write((Int32)Convert.ToInt32(parts [1]));
                            writer.Write((string)parts [2]);
                            writer.Write((string)parts [3]);
                            writer.Write((byte)Convert.ToByte(parts [4]));
                       
                            break;
                        }
                    case Protocol.CMSG_SERVER_VERSION_REQUEST:
                        {
                            //Bei diesen Kommandos muss nichts passieren
                            //da sie nur aus der ID bestehen.
                            break;
                        }
                    default:
                        {
                            Logger.Write(LogLevel.Warning, "Unimplemended command ({0}) in function InterpretMessage.", cmdValue);
                            break;
                        }
                }

                return new MessageIn(stream.ToArray());
            } catch
            {
                //0x7FFF XXX Invalid Message in die Message schreiben
                Logger.Write(LogLevel.Warning, "Invalid message recieved.");

                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);

                //Bytepaket zusammenbauen
                writer.Write((UInt16)Protocol.XXMSG_INVALID);

                return new MessageIn(stream.ToArray());
            }
        }

        byte[] ReadWebsocketPackage()
        {
            byte[] buffer = new byte[2];
            baseStream.Read(buffer, 0, 2);

            bool fin = (buffer [0] & 0x80) == 0x80;

            bool rsv1 = (buffer [0] & 0x40) == 0x40;
            bool rsv2 = (buffer [0] & 0x20) == 0x20;
            bool rsv3 = (buffer [0] & 0x10) == 0x10;

            int opCode = ((buffer [0] & 0x8) | (buffer [0] & 0x4) | (buffer [0] & 0x2) | (buffer [0] & 0x1));

            bool mask = (buffer [1] & 0x80) == 0x80;

            byte payload = (byte)((buffer [1] & 0x40) | (buffer [1] & 0x20) | (buffer [1] & 0x10) | (buffer [1] & 0x8) | (buffer [1] & 0x4) | (buffer [1] & 0x2) | (buffer [1] & 0x1));
            ulong length = 0;

            switch (payload)
            {
                case 126:
                    {
                        buffer = new byte[2];
                        baseStream.Read(buffer, 0, 2);
                        byte[] bytesUShort = buffer;
                        if (bytesUShort != null)
                        {
                            Array.Reverse(bytesUShort);
                            length = BitConverter.ToUInt16(bytesUShort, 0);
                        }
                        break;
                    }
                case 127:
                    {
                        buffer = new byte[8];
                        baseStream.Read(buffer, 0, 8);
                        byte[] bytesULong = buffer;
                        if (bytesULong != null)
                        {
                            Array.Reverse(bytesULong);
                            length = BitConverter.ToUInt16(bytesULong, 0);
                        }
                        break;
                    }
                default:
                    {
                        length = payload;
                        break;
                    }
            }

            byte[] maskKeys = null;
            if (mask)
            {
                buffer = new byte[4];
                baseStream.Read(buffer, 0, 4);
                maskKeys = buffer;
            }

            buffer = new byte[length];
            baseStream.Read(buffer, 0, (int)length);
            byte[] data = buffer;

            if (mask)
            {
                for (int i=0; i<data.Length; ++i)
                {
                    data [i] = (byte)(data [i] ^ maskKeys [i % 4]);
                }
            }

            return data;
        }
    }
}

