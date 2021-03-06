using System;
using System.Collections.Generic;

namespace ISL.Server.Common
{
/**
 * Structure storing the equipment and inventory of a Player.
 */
    public class Possessions
    {
        Dictionary< uint, InventoryItem > inventory=new Dictionary<uint, InventoryItem>();
        Dictionary< uint, EquipmentItem > equipSlots=new Dictionary<uint, EquipmentItem>();

        public Dictionary< uint, EquipmentItem > getEquipment()
        {
            return equipSlots;
        }

        public Dictionary< uint, InventoryItem > getInventory()
        {
            return inventory;
        }

        /**
     * Should be done only at character serialization and storage load time.
     */
        public void setEquipment(Dictionary< uint, EquipmentItem > equipData)
        { 
            //equipSlots.swap(equipData); }

            foreach(KeyValuePair<uint, EquipmentItem> pair in equipData)
            {
                equipSlots[pair.Key]=pair.Value;
            }
        }

        public void setInventory(Dictionary< uint, InventoryItem > inventoryData)
        { 
            //inventory.swap(inventoryData); }

            foreach(KeyValuePair<uint, InventoryItem> pair in inventoryData)
            {
                inventory[pair.Key]=pair.Value;
            }
        }
    }
}
