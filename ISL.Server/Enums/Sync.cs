using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Sync
	{
		// used to identify part of sync message
		SYNC_CHARACTER_POINTS=0x01,     // D charId, D charPoints, D corrPoints
		SYNC_CHARACTER_ATTRIBUTE=0x02,       // D charId, D attrId, DF base, DF mod
		SYNC_CHARACTER_SKILL=0x03,       // D charId, B skillId, D skill value
		SYNC_ONLINE_STATUS=0x04       // D charId, B 0 = offline, 1 = online
	}
}
