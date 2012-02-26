﻿using System;
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
			mPeer=peer;
		}

		bool isConnected()
		{
			// return (mPeer->state == ENET_PEER_STATE_CONNECTED);
			return true;
		}

		void disconnect(MessageOut msg)
		{
			if(isConnected())
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
			send(msg, true, 0);
		}

		public void send(MessageOut msg, bool reliable, uint channel)
		{
			Logger.Add(LogLevel.Debug, "Sending message {0} to {1}", msg, this);

			//gBandwidth->increaseClientOutput(this, msg.getLength());

			//TODO Überprüfung ob Länge größer ushort dann Problem
			NetworkStream stream=mPeer.GetStream();
			stream.Write(msg.getData(), 0, (int)msg.getLength()); 

			//if(packet)
			//{
			//    enet_peer_send(mPeer, channel, packet);
			//}
			//else
			//{
			//    LOG_ERROR("Failure to create packet!");
			//}
		}

		//std::ostream &operator <<(std::ostream &os, const NetComputer &comp)
		//{
		//    // address.host contains the ip-address in network-byte-order
		//    if (utils::processor::isLittleEndian)
		//        os << ( comp.mPeer->address.host & 0x000000ff)        << "."
		//           << ((comp.mPeer->address.host & 0x0000ff00) >> 8)  << "."
		//           << ((comp.mPeer->address.host & 0x00ff0000) >> 16) << "."
		//           << ((comp.mPeer->address.host & 0xff000000) >> 24);
		//    else
		//    // big-endian
		//    // TODO: test this
		//        os << ((comp.mPeer->address.host & 0xff000000) >> 24) << "."
		//           << ((comp.mPeer->address.host & 0x00ff0000) >> 16) << "."
		//           << ((comp.mPeer->address.host & 0x0000ff00) >> 8)  << "."
		//           << ((comp.mPeer->address.host & 0x000000ff));

		//    return os;
		//}

		int getIP()
		{
			//return mPeer->address.host;
			return 0; //ssk;
		}

		public override string ToString()
		{
			IPEndPoint remoteEndPoint=(IPEndPoint)(mPeer.Client.RemoteEndPoint);
			return String.Format("{0}:{1}", remoteEndPoint.Address, remoteEndPoint.Port);
		}
	}
}