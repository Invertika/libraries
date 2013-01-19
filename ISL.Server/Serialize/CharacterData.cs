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
    public static class CharacterData
    {
        public static void serializeCharacterData(Character data, MessageOut msg)
        {
            // general character properties
            msg.writeInt8(data.getAccountLevel());
            msg.writeInt8(data.getGender());
            msg.writeInt8(data.getHairStyle());
            msg.writeInt8(data.getHairColor());
            msg.writeInt16(data.getLevel());
            msg.writeInt16(data.getCharacterPoints());
            msg.writeInt16(data.getCorrectionPoints());

            msg.writeInt16(data.mAttributes.Count);

            foreach(KeyValuePair<uint, AttributeValue> pair in data.mAttributes)
            {
                msg.writeInt16((Int16)pair.Key);

                msg.writeDouble(pair.Value.@base);
                msg.writeDouble(pair.Value.modified);
            }


            // character skills
            msg.writeInt16(data.getSkillSize());

            foreach(KeyValuePair<int, int> pair in data.mExperience)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt32(pair.Value);
            }

            // status effects currently affecting the character
            msg.writeInt16(data.getStatusEffectSize());

            foreach(KeyValuePair<int, int> pair in data.mStatusEffects)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt16(pair.Value);
            }

            // location
            msg.writeInt16(data.getMapId());
            Point pos=data.getPosition();
            msg.writeInt16(pos.x);
            msg.writeInt16(pos.y);

            // kill count
            msg.writeInt16(data.getKillCountSize());

            foreach(KeyValuePair<int, int> pair in data.mKillCount)
            {
                msg.writeInt16(pair.Key);
                msg.writeInt32(pair.Value);
            }

            // character specials
            msg.writeInt16(data.mSpecials.Count);

            foreach(KeyValuePair<int, Special> pair in data.mSpecials)
            {
                msg.writeInt32(pair.Key);
            }

            // inventory - must be last because size isn't transmitted
            Possessions poss=data.getPossessions();
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

        public static void deserializeCharacterData(Character data, MessageIn msg)
        {
            //// general character properties
            //data.setAccountLevel(msg.readInt8());
            //data.setGender(ManaServ.getGender(msg.readInt8()));
            //data.setHairStyle(msg.readInt8());
            //data.setHairColor(msg.readInt8());
            //data.setLevel(msg.readInt16());
            //data.setCharacterPoints(msg.readInt16());
            //data.setCorrectionPoints(msg.readInt16());

            //// character attributes
            //uint attrSize = (uint)msg.readInt16();
            //for (uint i = 0; i < attrSize; ++i)
            //{
            //    uint id = msg.readInt16();
            //    double @base = msg.readDouble(),
            //           mod  = msg.readDouble();
            //    data.setAttribute(id, @base);
            //    data.setModAttribute(id, mod);
            //}

            //// character skills
            //int skillSize = msg.readInt16();

            //for (int i = 0; i < skillSize; ++i)
            //{
            //    int skill = msg.readInt16();
            //    int level = msg.readInt32();
            //    data.setExperience(skill,level);
            //}

            //// status effects currently affecting the character
            //int statusSize = msg.readInt16();

            //for (int i = 0; i < statusSize; i++)
            //{
            //    int status = msg.readInt16();
            //    int time = msg.readInt16();
            //    data.applyStatusEffect(status, time);
            //}

            //// location
            //data.setMapId(msg.readInt16());

            //Point temporaryPoint;
            //temporaryPoint.x = msg.readInt16();
            //temporaryPoint.y = msg.readInt16();
            //data.setPosition(temporaryPoint);

            //// kill count
            //int killSize = msg.readInt16();
            //for (int i = 0; i < killSize; i++)
            //{
            //    int monsterId = msg.readInt16();
            //    int kills = msg.readInt32();
            //    data.setKillCount(monsterId, kills);
            //}

            //// character specials
            //int specialSize = msg.readInt16();
            //data.clearSpecials();
            //for (int i = 0; i < specialSize; i++)
            //{
            //    data.giveSpecial(msg.readInt32());
            //}


            //Possessions &poss = data.getPossessions();
            //EquipData equipData;
            //int equipSlotsSize = msg.readInt16();
            //uint eqSlot;
            //EquipmentItem equipItem;
            //for (int j = 0; j < equipSlotsSize; ++j)
            //{
            //    eqSlot  = msg.readInt16();
            //    equipItem.itemId = msg.readInt16();
            //    equipItem.itemInstance = msg.readInt16();
            //    equipData.insert(equipData.end(),
            //                           std::make_pair(eqSlot, equipItem));
            //}
            //poss.setEquipment(equipData);

            //// Loads inventory - must be last because size isn't transmitted
            //Dictionary<uint, InventoryItem > inventoryData;
            //while (msg.getUnreadLength())
            //{
            //    InventoryItem i;
            //    int slotId = msg.readInt16();
            //    i.itemId   = msg.readInt16();
            //    i.amount   = msg.readInt16();
            //    inventoryData.insert(inventoryData.end(), std::make_pair(slotId, i));
            //}
            //poss.setInventory(inventoryData);
        }
    }
}
