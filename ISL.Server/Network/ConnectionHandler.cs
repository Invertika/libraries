//
//  ConnectionHandler.cs
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
using System.Threading;
using ISL.Server.Utilities;
using ISL.Server.Common;

namespace ISL.Server.Network
{
	public class ConnectionHandler
	{
		/**
		 * A list of pointers to the client structures created by
		 * computerConnected.
		 */
		public List<NetComputer> clients;

		//ENetAddress address;      /**< Includes the port to listen to. */
		//ENetHost *host;           /**< The host that listen for connections. */

		//TcpListener

		ushort Port=0;
		string ListenHost="";

		public ConnectionHandler()
		{
			clients=new List<NetComputer>();
		}

		protected virtual NetComputer computerConnected(TcpClient peer)
		{
			throw new NotImplementedException("These function must be overloaded from derived class.");
		}

		protected virtual void computerDisconnected(NetComputer comp)
		{
			throw new NotImplementedException("These function must be overloaded from derived class.");
		}

		protected virtual void processMessage(NetComputer comp, MessageIn msg)
		{
			throw new NotImplementedException("These function must be overloaded from derived class.");
		}

		public bool startListen(ushort port, string listenHost)
		{
			Port=port;
			ListenHost=listenHost;

			//    // Bind the server to the default localhost.
			//    address.host = ENET_HOST_ANY;
			//    address.port = port;

			//    if (!listenHost.empty())
			//        enet_address_set_host(&address, listenHost.c_str());

			//    LOG_INFO("Listening on port " << port << "...");
			//#if defined(ENET_VERSION) && ENET_VERSION >= ENET_CUTOFF
			//    host = enet_host_create(
			//            &address    /* the address to bind the server host to */,
			//            Configuration::getValue("net_maxClients", 1000) /* allowed connections */,
			//            0           /* unlimited channel count */,
			//            0           /* assume any amount of incoming bandwidth */,
			//            0           /* assume any amount of outgoing bandwidth */);
			//#else
			//    host = enet_host_create(
			//            &address    /* the address to bind the server host to */,
			//            Configuration::getValue("net_maxClients", 1000) /* allowed connections */,
			//            0           /* assume any amount of incoming bandwidth */,
			//            0           /* assume any amount of outgoing bandwidth */);
			//#endif

			//    return host != 0;

			return true; //ssk
		}

		public void stopListen()
		{
			//// - Disconnect all clients (close sockets)

			//// TODO: probably there's a better way.
			//ENetPeer *currentPeer;

			//for (currentPeer = host->peers;
			//     currentPeer < &host->peers[host->peerCount];
			//     ++currentPeer)
			//{
			//   if (currentPeer->state == ENET_PEER_STATE_CONNECTED)
			//   {
			//        enet_peer_disconnect(currentPeer, 0);
			//        enet_host_flush(host);
			//        enet_peer_reset(currentPeer);
			//   }
			//}
			//enet_host_destroy(host);
			//// FIXME: memory leak on NetComputers
		}

		public void flush()
		{
			//enet_host_flush(host);
		}

		//TODO Extension oder Abgeleitete Klasse
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

		private void HandleClient(object td)
		{
			NetComputer comp=(NetComputer)td;
			TcpClient peer=comp.Peer;

			// If the scripting subsystem didn't hook the message
			// it will be handled by the default message handler.

			String data=null;
			//Byte[] bytes=new Byte[256];
			Byte[] length=new Byte[2];
			int i;

			// Get a stream object for reading and writing
			NetworkStream stream=peer.GetStream();

			//// Loop to receive all the data sent by the client.
			//while((i=stream.Read(bytes, 0, bytes.Length))!=0)
			while((i=stream.Read(length, 0, length.Length))!=0) //TODO Auf ReadSecure umbiegen
			{
				ushort commandLength=(ushort)BitConverter.ToUInt16(length, 0);

				//Empfange Kommando
				//Byte[] commandData=new Byte[commandLength];
				//int readed=stream.Read(commandData, 0, commandData.Length);
				//int readed=
				byte[] commandData=commandData=stream.Read(commandLength);
				//stream.Read(commandData, 0, commandData.Length);


				//TODO Wartemn bis Menge wirklich komplett
				//Packe Kommando in MessageIn
				MessageIn msg=new MessageIn(commandData);

				Logger.Add(LogLevel.Debug, "Received message {0} from {1}", (Protocol)msg.getId(), comp);

				processMessage(comp, msg);
			}
			
			//Disconnect
			IPEndPoint remoteEndPoint=(IPEndPoint)(peer.Client.RemoteEndPoint);
			Logger.Add(LogLevel.Information, "Client {0}:{1} disconnected", remoteEndPoint.Address, remoteEndPoint.Port);

			// Reset the peer's client information.
			computerDisconnected(comp);
			clients.Remove(comp);
		}

		public void process()
		{
			process(0);
		}

		public void process(uint timeout)
		{
			TcpListener server=null;
			try
			{
				// Set the TcpListener on port 13000.
				Int32 port=Port;
				//IPAddress localAddr=IPAddress.Parse(ListenHost); //TODO Überprüfen
				IPAddress localAddr=IPAddress.Parse("127.0.0.1");

				// TcpListener server = new TcpListener(port);
				server=new TcpListener(localAddr, port);

				// Start listening for client requests.
				server.Start();

				// Enter the listening loop.
				while(true)
				{
					Logger.Add(LogLevel.Information, "Waiting for a connection...");

					// Perform a blocking call to accept requests.
					// You could also user server.AcceptSocket() here.
					TcpClient client=server.AcceptTcpClient();

					//Cast remote end point
					IPEndPoint remoteEndPoint=(IPEndPoint)(client.Client.RemoteEndPoint);

					NetComputer comp=computerConnected(client);
					clients.Add(comp);
					Logger.Add(LogLevel.Information, "A new client connected from {0}:{1} to port {2}", remoteEndPoint.Address, remoteEndPoint.Port, port);

					//Client to thread
					Thread clientThread;	// Der Thread in dem die Process Funktion läuft

					clientThread=new Thread(HandleClient);
					clientThread.Name="Client Thread";
					clientThread.Start(comp);
				}
			}
			catch(SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
			finally
			{
				// Stop listening for new clients.
				server.Stop();
			}

			//ENetEvent event;
			//// Process Enet events and do not block.
			//while (enet_host_service(host, &event, timeout) > 0) {
			//    switch (event.type) {
			////////////        case ENET_EVENT_TYPE_CONNECT:
			////////////        {
			////////////            NetComputer *comp = computerConnected(event.peer);
			////////////            clients.push_back(comp);
			////////////            LOG_INFO("A new client connected from " << *comp << ":"
			////////////                     << event.peer->address.port << " to port "
			////////////                     << host->address.port);

			////////////            // Store any relevant client information here.
			////////////            event.peer->data = (void *)comp;
			////////////        } break;

			//        case ENET_EVENT_TYPE_RECEIVE:
			//        {
			//            NetComputer *comp =
			//                static_cast<NetComputer*>(event.peer->data);

			//            // If the scripting subsystem didn't hook the message
			//            // it will be handled by the default message handler.

			//            // Make sure that the packet is big enough (> short)
			//            if (event.packet->dataLength >= 2) {
			//                MessageIn msg((char *)event.packet->data,
			//                              event.packet->dataLength);
			//                LOG_DEBUG("Received message " << msg << " from "
			//                          << *comp);

			//                gBandwidth->increaseClientInput(comp, event.packet->dataLength);

			//                processMessage(comp, msg);
			//            } else {
			//                LOG_ERROR("Message too short from " << *comp);
			//            }

			//            /* Clean up the packet now that we're done using it. */
			//            enet_packet_destroy(event.packet);
			//        } break;

			//        case ENET_EVENT_TYPE_DISCONNECT:
			//        {
			//            NetComputer *comp =
			//                static_cast<NetComputer*>(event.peer->data);

			//            LOG_INFO("" << *comp << " disconnected.");

			//            // Reset the peer's client information.
			//            computerDisconnected(comp);
			//            clients.erase(std::find(clients.begin(), clients.end(), comp));
			//            event.peer->data = NULL;
			//        } break;

			//        default: break;
			//    }
			//}
		}

		void sendToEveryone(MessageOut msg)
		{
			//for (NetComputers::iterator i = clients.begin(), i_end = clients.end();
			//     i != i_end; ++i)
			//{
			//    (*i)->send(msg);
			//}
		}

		uint getClientCount()
		{
			return (uint)clients.Count;
		}
	}
}
