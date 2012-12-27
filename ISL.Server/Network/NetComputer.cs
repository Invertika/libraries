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
using CSCL;
using System.IO;

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
            return mPeer.Connected;
        }

        public void disconnect(MessageOut msg)
        {
            if(isConnected())
            {
                mPeer.Close();
            }
        }

        public void send(MessageOut msg)
        {
            Logger.Write(LogLevel.Debug, "Sending message {0} to {1}", msg, this);

            NetworkStream stream=mPeer.GetStream();

            //Daten in Websocketformat verpacken
            byte[] wsMsg=Websocket.GetWebsocketDataFrame(msg.getData());

            try
            {
                stream.Write(wsMsg);
            }
            catch(IOException ex)
            {
                if(((SocketException)(ex.InnerException)).ErrorCode==10053)
                {
                    Logger.Write(LogLevel.Warning, "An established connection was aborted by the software in your host machine.");
                }
            }
        }

        public IPAddress getIP()
        {
            IPEndPoint remoteEndPoint=(IPEndPoint)(mPeer.Client.RemoteEndPoint);
            return remoteEndPoint.Address;
        }

        public override string ToString()
        {
            IPEndPoint remoteEndPoint=(IPEndPoint)(mPeer.Client.RemoteEndPoint);
            return String.Format("{0}:{1}", remoteEndPoint.Address, remoteEndPoint.Port);
        }
    }
}
