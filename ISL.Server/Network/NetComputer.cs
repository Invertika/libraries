//
//  NetComputer.cs
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
using System.Net;
using ISL.Server.Utilities;

namespace ISL.Server.Network
{
    public class NetComputer
    {
        public TcpClient Peer
        {
            get
            {
                return mPeer;
            }
        }

        TcpClient mPeer;

        public NetComputer(TcpClient peer)
        {
            mPeer = peer;
        }

        bool isConnected()
        {
            // return (mPeer.state == ENET_PEER_STATE_CONNECTED);
            return true;
        }

        public void disconnect(MessageOut msg)
        {
            if (isConnected())
            {
                ///* ChannelID 0xFF is the channel used by enet_peer_disconnect.
                // * If a reliable packet is send over this channel ENet guaranties
                // * that the message is recieved before the disconnect request.
                // */
                //send(msg, ENET_PACKET_FLAG_RELIABLE, 0xFF);

                ///* ENet generates a disconnect event
                // * (notifying the connection handler).
                // */
                //enet_peer_disconnect(mPeer, 0);
            }
        }

		public void send(MessageOut msg)
		{
			Logger.Write(LogLevel.Debug, "Sending message {0} to {1}", msg, this);

			NetworkStream stream=mPeer.GetStream();

			string msgString=Websocket.GetWebsocketMessage(msg);

			System.Text.UTF8Encoding enc=new System.Text.UTF8Encoding();
			byte[] wsMsg=Websocket.GetWebsocketDataFrame(enc.GetBytes(msgString));
			stream.Write(wsMsg);
		}

		//public void send(MessageOut msg)
		//{
		//    Logger.Write(LogLevel.Debug, "Sending message {0} to {1}", msg, this);

		//    //gBandwidth.increaseClientOutput(this, msg.getLength());

		//    NetworkStream stream = mPeer.GetStream();

		//    //Länge senden
		//    ushort lengthPackage = (ushort)msg.getLength();
		//    byte[] lengthAsByteArray = BitConverter.GetBytes(lengthPackage);
		//    stream.Write(lengthAsByteArray, 0, (int)lengthAsByteArray.Length); 

		//    //TODO Überprüfung ob Länge größer ushort dann Problem
		//    stream.Write(msg.getData(), 0, (int)msg.getLength()); 

		//    //if(packet)
		//    //{
		//    //    enet_peer_send(mPeer, channel, packet);
		//    //}
		//    //else
		//    //{
		//    //    LOG_ERROR("Failure to create packet!");
		//    //}
		//}

        //std::ostream &operator <<(std::ostream &os, const NetComputer &comp)
        //{
        //    // address.host contains the ip-address in network-byte-order
        //    if (utils::processor::isLittleEndian)
        //        os << ( comp.mPeer.address.host & 0x000000ff)        << "."
        //           << ((comp.mPeer.address.host & 0x0000ff00) >> 8)  << "."
        //           << ((comp.mPeer.address.host & 0x00ff0000) >> 16) << "."
        //           << ((comp.mPeer.address.host & 0xff000000) >> 24);
        //    else
        //    // big-endian
        //    // TODO: test this
        //        os << ((comp.mPeer.address.host & 0xff000000) >> 24) << "."
        //           << ((comp.mPeer.address.host & 0x00ff0000) >> 16) << "."
        //           << ((comp.mPeer.address.host & 0x0000ff00) >> 8)  << "."
        //           << ((comp.mPeer.address.host & 0x000000ff));

        //    return os;
        //}

        public IPAddress getIP()
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)(mPeer.Client.RemoteEndPoint);
            return remoteEndPoint.Address;
        }

        public override string ToString()
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)(mPeer.Client.RemoteEndPoint);
            return String.Format("{0}:{1}", remoteEndPoint.Address, remoteEndPoint.Port);
        }
    }
}
