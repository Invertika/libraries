//
//  CharacterData.cs
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
using ISL.Server.Utilities;
using ISL.Server.Account;
using ISL.Server.Game;
using ISL.Server.Common;

namespace ISL.Server.Serialize
{
    public class CharacterData
    {
        public byte mAccountLevel; //!< Level of the associated account.
        byte mGender;    //!< Gender of the being.
        byte mHairStyle; //!< Hair style of the being.
        byte mHairColor; //!< Hair color of the being.
        short mLevel;             //!< Level of the being.
        short mCharacterPoints;   //!< Unused character points.
        short mCorrectionPoints;  //!< Unused correction points.
        public Dictionary<uint, AttributeValue> mAttributes=new Dictionary<uint, AttributeValue>(); //!< Attributes.
        public Dictionary<int, int> mExperience=new Dictionary<int, int>(); //!< Skill Experience.
        public Dictionary<int, int> mStatusEffects=new Dictionary<int, int>(); //!< Status Effects
        ushort mMapId;    //!< Map the being is on.
        Point mPos;               //!< Position the being is at.
        public Dictionary<int, int> mKillCount=new Dictionary<int, int>(); //!< Kill Count
        public Dictionary<int, Special>  mSpecials=new Dictionary<int, Special>();
        Possessions mPossessions=new Possessions(); //!< All the possesions of the character.

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

        public int getStatusEffectSize()
        {
            return mStatusEffects.Count;
        }

        /**
        * Gets the Id of the map that the character is on.
        */
        public int getMapId()
        {
            return mMapId;
        }
        
        public Point getPosition()
        {
            return mPos;
        }

        public void setPosition(Point p)
        {
            mPos=p;
        }
        
        public int getKillCountSize()
        {
            return mKillCount.Count;
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

        public void giveSpecial(int id)
        {
            mSpecials.Add(id, null);
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

        void clearSpecials()
        {
            //for (std::map<int, Special*>::iterator i = mSpecials.begin();
            //     i != mSpecials.end(); i++)
            //{
            //    delete i.second;
            //}
            mSpecials.Clear();
        }

        public void serializeCharacterData(MessageOut msg)
        {
            // general character properties
            msg.writeInt8(getAccountLevel());
            msg.writeInt8(getGender());
            msg.writeInt8(getHairStyle());
            msg.writeInt8(getHairColor());
            msg.writeInt16(getLevel());
            msg.writeInt16(getCharacterPoints());
            msg.writeInt16(getCorrectionPoints());

            msg.writeInt16(mAttributes.Count);

            foreach(KeyValuePair<uint, AttributeValue> pair in mAttributes)
            {
                msg.writeInt16((Int16)pair.Key);

                msg.writeDouble(pair.Value.@base);
                msg.writeDouble(pair.Value.modified);
            }

            // character skills
            msg.writeInt16(getSkillSize());

            foreach(KeyValuePair<int, int> pair in mExperience)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt32(pair.Value);
            }

            // status effects currently affecting the character
            msg.writeInt16(getStatusEffectSize());

            foreach(KeyValuePair<int, int> pair in mStatusEffects)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt16(pair.Value);
            }

            // location
            msg.writeInt16(getMapId());
            Point pos=getPosition();
            msg.writeInt16(pos.x);
            msg.writeInt16(pos.y);

            // kill count
            msg.writeInt16(getKillCountSize());

            foreach(KeyValuePair<int, int> pair in mKillCount)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt32(pair.Value);
            }

            // character specials
            msg.writeInt16(mSpecials.Count);

            foreach(KeyValuePair<int, Special> pair in mSpecials)
            {
                msg.writeInt32(pair.Key);
            }

            // inventory - must be last because size isn't transmitted
            Possessions poss=getPossessions();
            Dictionary< uint, EquipmentItem > equipData=poss.getEquipment();
            msg.writeInt16(equipData.Count); // number of equipment

            foreach(KeyValuePair<uint, EquipmentItem> k in equipData)
            {
                msg.writeInt16((int)k.Key);                 // Equip slot id
                msg.writeInt16((int)k.Value.itemId);         // ItemId
                msg.writeInt16((int)k.Value.itemInstance);   // Item Instance id
            }

            Dictionary< uint, InventoryItem > inventoryData=poss.getInventory();

            foreach(KeyValuePair<uint, InventoryItem> j in inventoryData)
            {
                msg.writeInt16((int)j.Key);           // slot id
                msg.writeInt16((int)j.Value.itemId);   // item id
                msg.writeInt16((int)j.Value.amount);   // amount
            }
        }

        public void deserializeCharacterData(MessageIn msg)
        {
            // general character properties
            setAccountLevel(msg.readInt8());
            setGender(msg.readInt8());
            setHairStyle(msg.readInt8());
            setHairColor(msg.readInt8());
            setLevel(msg.readInt16());
            setCharacterPoints(msg.readInt16());
            setCorrectionPoints(msg.readInt16());

            // character attributes
            uint attrSize=(uint)msg.readInt16();
            for(uint i = 0;i < attrSize;++i)
            {
                uint id=(uint)msg.readInt16();
                double @base=msg.readDouble(),
                mod=msg.readDouble();
                setAttribute(id,  @base);
                setModAttribute(id, mod);
            }

            // character skills
            int skillSize=msg.readInt16();

            for(int i = 0;i < skillSize;++i)
            {
                int skill=msg.readInt16();
                int level=msg.readInt32();
                setExperience(skill, level);
            }

            // status effects currently affecting the character
            int statusSize=msg.readInt16();

            for(int i = 0;i < statusSize;i++)
            {
                int status=msg.readInt16();
                int time=msg.readInt16();
                applyStatusEffect(status, time);
            }

            // location
            setMapId(msg.readInt16());

            Point temporaryPoint=new Point();
            temporaryPoint.x=msg.readInt16();
            temporaryPoint.y=msg.readInt16();
            setPosition(temporaryPoint);

            // kill count
            int killSize=msg.readInt16();
            for(int i = 0;i < killSize;i++)
            {
                int monsterId=msg.readInt16();
                int kills=msg.readInt32();
                setKillCount(monsterId, kills);
            }

            // character specials
            int specialSize=msg.readInt16();
            clearSpecials();
            for(int i = 0;i < specialSize;i++)
            {
                giveSpecial(msg.readInt32());
            }

            Possessions poss=getPossessions();
            Dictionary< uint, EquipmentItem > equipData=new Dictionary<uint, EquipmentItem>();
            int equipSlotsSize=msg.readInt16();
            uint eqSlot;
            EquipmentItem equipItem=new EquipmentItem();
            for(int j = 0;j < equipSlotsSize;++j)
            {
                eqSlot=(uint)msg.readInt16();
                equipItem.itemId=(uint)msg.readInt16();
                equipItem.itemInstance=(uint)msg.readInt16();
                equipData.Add(eqSlot, equipItem);
            }
            poss.setEquipment(equipData);

            // Loads inventory - must be last because size isn't transmitted
            Dictionary<uint, InventoryItem > inventoryData=new Dictionary<uint, InventoryItem>();
            while(msg.getUnreadLength()>0)
            {
                InventoryItem i=new InventoryItem();
                int slotId=msg.readInt16();
                i.itemId=(uint)msg.readInt16();
                i.amount=(uint)msg.readInt16();
                inventoryData.Add((uint)slotId, i);
            }

            poss.setInventory(inventoryData);
        }
    }
}
