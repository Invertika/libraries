//
//  ThingType.cs
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
