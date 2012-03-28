using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Common
{
	public class EquipmentItem
	{
		EquipmentItem()
		{ 
		}

		EquipmentItem(uint itemId, uint itemInstance)
		{
			this.itemId=itemId;
			this.itemInstance=itemInstance;
		}

		// The item id taken from the item db.
		uint itemId;
		// A unique instance number used to separate items when equipping the same
		// item id multiple times on possible multiple slots.
		uint itemInstance;
	}
}
