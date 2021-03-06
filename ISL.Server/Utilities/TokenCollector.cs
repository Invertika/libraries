//
//  TokenCollector.cs
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
using ISL.Server.Network;

namespace ISL.Server.Utilities
{
    public class TokenCollector: TokenCollectorBase
    {
        ITokenCollectorHandler mHandler;

        public TokenCollector(ITokenCollectorHandler handler)//, NetComputer client, object serverData)
        {
            mHandler=handler;
        }

        //TokenCollector(Handler *h): mHandler(h)
        //{
        //    _TC_CheckData<Client> ClientMustBeSimple;
        //    (void)&ClientMustBeSimple;
        //    _TC_CheckData<ServerData> ServerDataMustBeSimple;
        //    (void)&ServerDataMustBeSimple;
        //}

        /**
		 * Checks if the server expected this client token. If so, calls
		 * Handler::tokenMatched. Otherwise marks the client as pending.
		 */
        public void addPendingClient(string token, NetComputer data)
        {
            insertClient(token, data); 
        }

        /**
		 * Checks if a client already registered this token. If so, calls
		 * Handler::tokenMatched. Otherwise marks the data as pending.
		 */
        public void addPendingConnect(string token, object data)
        {
            insertConnect(token, (object)data); 
        }

        /**
		 * Removes a pending client.
		 * @note Does not call destroyPendingClient.
		 */
        public void deletePendingClient(NetComputer data)
        {
            removeClient((object)data); 
        }

        protected override void removedClient(NetComputer data)
        {
            mHandler.deletePendingClient(data);
        }
        
        protected override void removedConnect(object data)
        {
            mHandler.deletePendingConnect(data);
            //mHandler.deletePendingConnect((ServerData)data);
        }
        
        protected override void foundMatch(NetComputer client, object data)
        {
            mHandler.tokenMatched(client, data);
        }
    }
}
