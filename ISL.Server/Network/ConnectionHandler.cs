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

        // A list of pointers to the client structures created by computerConnected.
        public List<NetComputer> clients;

        //ENetAddress address;      /**< Includes the port to listen to. */
        //ENetHost *host;           /**< The host that listen for connections. */

        TcpListener listener;

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

        /**
         * Open the server socket.
         * @param port the port to listen to
         * @host  the host IP to listen on, defaults to the default localhost
         */
        public bool startListen(ushort port, string listenHost="")
        {
            Port=port;
            ListenHost=listenHost;

            IPAddress localAddr=IPAddress.Parse("127.0.0.1");
            
            // TcpListener server = new TcpListener(port);
            listener=new TcpListener(localAddr, Port);
            
            // Start listening for client requests.
            try
            {
                listener.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void stopListen()
        {
            listener.Stop();
        }

        public void flush()
        {
            //enet_host_flush(host);
        }

        private void HandleClient(object td)
        {
            NetComputer comp=(NetComputer)td;
            TcpClient peer=comp.Peer;

            // If the scripting subsystem didn't hook the message
            // it will be handled by the default message handler.

            // Get a stream object for reading and writing
            NetworkStream stream=peer.GetStream();

            WebSocketReader reader=new WebSocketReader(stream);

            bool websocketClosed=false;

            while(websocketClosed==false)
            {
                MessageIn msg=reader.ReadMessage(out websocketClosed);

                if(!websocketClosed)
                {
                    Logger.Write(LogLevel.Debug, "Received message {0} from {1}", (Protocol)msg.getId(), comp);
                    processMessage(comp, msg);
                }
            }
			
            //Disconnect
            IPEndPoint remoteEndPoint=(IPEndPoint)(peer.Client.RemoteEndPoint);
            Logger.Write(LogLevel.Information, "Client {0}:{1} disconnected", remoteEndPoint.Address, remoteEndPoint.Port);

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
            try
            {
                // Enter the listening loop.
                while(true)
                {
                    Logger.Write(LogLevel.Information, "Waiting for a connection...");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client=listener.AcceptTcpClient();

                    //Websocketbehandlung falls nötig (bei Client immer nötig)
                    Websocket.OnAccept(client);
                    //client.BeginAccept(null, 0, OnAccept, null);     

                    //Cast remote end point
                    IPEndPoint remoteEndPoint=(IPEndPoint)(client.Client.RemoteEndPoint);

                    NetComputer comp=computerConnected(client);
                    clients.Add(comp);

                    Logger.Write(LogLevel.Information, "A new client connected from {0}:{1} to port {2}", remoteEndPoint.Address, remoteEndPoint.Port, Port);

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
                listener.Stop();
            }
        }

        protected void sendToEveryone(MessageOut msg)
        {
            foreach(NetComputer comp in clients)
            {
                comp.send(msg);
            }
        }

        public uint getClientCount()
        {
            return (uint)clients.Count;
        }
    }
}
