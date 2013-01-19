//
//  Character.cs
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
using ISL.Server.Utilities;
using ISL.Server.Common;
using ISL.Server.Game;
using ISL.Server.Serialize;

namespace ISL.Server.Account
{
    public class Character
    {
        public CharacterData characterData;

        //Character(Character &);
        //Character &operator=(const Character &);

        //double getAttrBase(AttributeMap::const_iterator &it) const
        //{ return it.second.base; }
        //double getAttrMod(AttributeMap::const_iterator &it) const
        //{ return it.second.modified; }

        string mName;        //!< Name of the character.
        int mDatabaseID;          //!< Character database ID.
        uint mCharacterSlot;  //!< Character slot.
        int mAccountID;           //!< Account ID of the owner.
        Account mAccount;        //!< Account owning the character.

        //std::vector<std::string> mGuilds;        //!< All the guilds the player
        //!< belongs to.
        //friend class AccountHandler;
        //friend class Storage;
        // Set as a friend, but still a lot of redundant accessors. FIXME.
        //template< class T >
        //friend void serializeCharacterData(const T &data, MessageOut &msg);

        public Character(string name, int id=-1)
        {
            mName=name;
            mDatabaseID=id;
            mAccountID=-1;
            mAccount=null;
            characterData=new CharacterData();
        }

        public void setAccount(Account acc)
        {
            mAccount=acc;
            mAccountID=acc.getID();
            characterData.mAccountLevel=(byte)acc.getLevel();
        }

        /**
 * Gets the ID of the account the character belongs to.
 */
        public int getAccountID()
        {
            return mAccountID;
        }

        public void setAccountID(int id)
        {
            mAccountID=id;
        }

        /**
 * Gets the name of the character.
 */
        public string getName()
        {
            return mName;
        }

        void setName(string name)
        {
            mName=name;
        }

        /**
	  * Gets the database id of the character.
	  */
        public int getDatabaseID()
        {
            return mDatabaseID;
        }

        public void setDatabaseID(int id)
        {
            mDatabaseID=id;
        }

        /**
		 * Gets the slot of the character.
		 */
        public uint getCharacterSlot()
        {
            return mCharacterSlot;
        }

        public void setCharacterSlot(uint slot)
        {
            mCharacterSlot=slot;
        }


    }
}
