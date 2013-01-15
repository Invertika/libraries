//
//  TokenCollectorBase.cs
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
    public class TokenCollectorBase
    {
        /**
             * List containing client already connected. Newer clients are at the
             * back of the list.
             */
        List<TokenItem> mPendingClients;

        /**
             * List containing server data waiting for clients. Newer data are at
             * the back of the list.
             */
        List<TokenItem> mPendingConnects;

        /**
             * Time at which the TokenCollector performed its last check.
             */
        DateTime mLastCheck;

        public TokenCollectorBase()
        {
            mLastCheck=DateTime.MinValue;
            mPendingClients=new List<TokenItem>();
            mPendingConnects=new List<TokenItem>();
        }

        //protected:
        protected virtual void removedClient(NetComputer data)
        {
            throw new NotImplementedException("These function must be overloaded from derived class.");
        }

        protected virtual void removedConnect(object data)
        {
            throw new NotImplementedException("These function must be overloaded from derived class.");
        }

        protected virtual void foundMatch(NetComputer client, object data)
        {
            throw new NotImplementedException("These function must be overloaded from derived class.");
        }

        //    virtual void removedClient(intptr_t) = 0;
        //    virtual void removedConnect(intptr_t) = 0;
        //    virtual void foundMatch(intptr_t client, intptr_t connect) = 0;

        protected void insertClient(string token, object data)
        {
            foreach(TokenItem item in mPendingConnects)
            {
                if(item.Token==token)
                {
                    foundMatch((NetComputer)data, item.Data);
                    mPendingConnects.Remove(item); //mPendingConnects.erase(--it.base());
                    return;
                }
            }

            DateTime current=DateTime.Now;

            TokenItem tItem=new TokenItem(token, data, current);
            mPendingClients.Add(tItem);

            removeOutdated(current);
        }

        protected void insertConnect(string token, object data) //intptr_t data)
        {
            foreach(TokenItem item in mPendingClients)
            {
                if(item.Token==token)
                {
                    foundMatch((NetComputer)item.Data, data);
                    mPendingClients.Remove(item);
                    return;
                }
            }

            DateTime current=DateTime.Now;

            TokenItem tItem=new TokenItem(token, data, current);
            mPendingConnects.Add(tItem);

            removeOutdated(current);
        }

        protected void removeClient(object data) //intptr_t data)
        {
            foreach(TokenItem item in mPendingClients)
            {
                if(item.Data==data)
                {
                    mPendingClients.Remove(item);
                    return;
                }
            }
        }

        protected void removeOutdated(DateTime current) //time_t
        {
            // Timeout happens after 30 seconds. Much longer may actually pass, though.
            DateTime threshold=current-new TimeSpan(0, 0, 0, 30, 0);
            if(threshold<mLastCheck)
                return;

            for(int i=0;i<mPendingConnects.Count;i++)
            {
                TokenItem item=mPendingConnects[i];

                if(item.TimeStamp<threshold)
                {
                    removedConnect(item.Data);
                    mPendingConnects.Remove(item);
                }
            }

            foreach(TokenItem item in mPendingClients)
            {
                if(item.TimeStamp<threshold)
                {
                    removedClient((NetComputer)item.Data);
                    mPendingClients.Remove(item);
                }
            }
    
            mLastCheck=current;
        }
    }
}
