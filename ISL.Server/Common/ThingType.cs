using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Common
{
	// Object type enumeration
	public enum ThingType
	{
		// A simple item.
		OBJECT_ITEM=0,
		// An item that toggle map/quest actions (doors, switchs, ...)
		// and can speak (map panels).
		OBJECT_ACTOR=1,
		// Non-Playable-Character is an actor capable of movement and maybe actions.
		OBJECT_NPC=2,
		// A monster (moving actor with AI. Should be able to toggle map/quest
		// actions, too).
		OBJECT_MONSTER=3,
		// A normal being.
		OBJECT_CHARACTER=4,
		// A effect to be shown.
		OBJECT_EFFECT=5,
		// Server-only object.
		OBJECT_OTHER=6
	}
}
