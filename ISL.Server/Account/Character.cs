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

namespace ISL.Server.Account
{
    public class Character
    {
        //Character(Character &);
        //Character &operator=(const Character &);

        //double getAttrBase(AttributeMap::const_iterator &it) const
        //{ return it.second.base; }
        //double getAttrMod(AttributeMap::const_iterator &it) const
        //{ return it.second.modified; }

        Possessions mPossessions=new Possessions(); //!< All the possesions of the character.
        string mName;        //!< Name of the character.
        int mDatabaseID;          //!< Character database ID.
        uint mCharacterSlot;  //!< Character slot.
        int mAccountID;           //!< Account ID of the owner.
        Account mAccount;        //!< Account owning the character.
        Point mPos;               //!< Position the being is at.
        public Dictionary<uint, AttributeValue> mAttributes=new Dictionary<uint, AttributeValue>(); //!< Attributes.
        public Dictionary<int, int> mExperience=new Dictionary<int, int>(); //!< Skill Experience.
        Dictionary<int, int> mStatusEffects; //!< Status Effects
        Dictionary<int, int> mKillCount; //!< Kill Count
        Dictionary<int, Special>  mSpecials;
        ushort mMapId;    //!< Map the being is on.
        byte mGender;    //!< Gender of the being.
        byte mHairStyle; //!< Hair style of the being.
        byte mHairColor; //!< Hair color of the being.
        short mLevel;             //!< Level of the being.
        short mCharacterPoints;   //!< Unused character points.
        short mCorrectionPoints;  //!< Unused correction points.
        byte mAccountLevel; //!< Level of the associated account.

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
        }

        public void setAccount(Account acc)
        {
            mAccount=acc;
            mAccountID=acc.getID();
            mAccountLevel=(byte)acc.getLevel();
        }

        /**
		* Gets the Id of the map that the character is on.
		*/
        public int getMapId()
        {
            return mMapId;
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

        /** Gets the account level of the user. */
        public int getAccountLevel()
        {
            return mAccountLevel;
        }

        /**
	   * Gets the gender of the character (male / female).
	   */
        public int getGender()
        {
            return mGender;
        }

        public void setGender(int gender)
        {
            mGender=(byte)gender;
        }

        /**
 * Gets the hairstyle of the character.
 */
        public int getHairStyle()
        {
            return mHairStyle;
        }

        public void setHairStyle(int style)
        {
            mHairStyle=(byte)style;
        }

        /**
 * Gets the haircolor of the character.
 */
        public int getHairColor()
        {
            return mHairColor;
        }
        public void setHairColor(int color)
        {
            mHairColor=(byte)color;
        }

        /**
 * Gets the level of the character.
 */
        public int getLevel()
        {
            return mLevel;
        }
        public void setLevel(int level)
        {
            mLevel=(byte)level;
        }

        public int getCharacterPoints()
        {
            return mCharacterPoints;
        }

        public int getCorrectionPoints()
        {
            return mCorrectionPoints;
        }

        public int getSkillSize()
        {
            return mExperience.Count;
        }

        public void setPosition(Point p)
        {
            mPos=p;
        }

        /**
         * Gets a reference on the possessions.
         */
        public Possessions getPossessions()
        {
            return mPossessions;
        }

        public void setCharacterPoints(int points)
        {
            mCharacterPoints=(short)points;
        }

        
        public void setCorrectionPoints(int points)
        {
            mCorrectionPoints=(short)points;
        }

        public void setMapId(int mapId)
        {
            mMapId=(ushort)mapId;
        }

        public void setCharacterSlot(uint slot)
        {
            mCharacterSlot=slot;
        }

        /**
          * Sets the account level of the user.
          * @param force ensure the level is not modified by a game server.
          */
        public void setAccountLevel(int l, bool force = false)
        {
            if(force)
                mAccountLevel=(byte)l;
        }

        /** Sets the value of a base attribute of the character. */
        public void setAttribute(uint id, double value)
        {
            mAttributes[id].@base=value;
        }
        
        public void setModAttribute(uint id, double value)
        {
            mAttributes[id].modified=value;
        }

        public void setExperience(int skill, int value)
        {
            mExperience[skill]=value;
        }

        /**
         * Get / Set a status effects
         */
        public void applyStatusEffect(int id, int time)
        {
            mStatusEffects[id]=time;
        }

        public void setKillCount(int monsterId, int kills)
        {
            mKillCount[monsterId]=kills;
        }

        public void giveSpecial(int id, int currentMana)
        {
            //mSpecials[id] = SpecialValue(currentMana);
            Special spec=new Special();
            spec.currentMana=currentMana;
            mSpecials[id]=spec;
            
            //TODO Gegen Originalimplementation checken

//            if (mSpecials.find(id) == mSpecials.end())
//            {
//                mSpecials[id] = SpecialValue(currentMana);
//            }
        }

        public Point getPosition()
        {
            return mPos;
        }
    }
}
