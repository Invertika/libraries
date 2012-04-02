//
//  Sync.cs
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
	/// <summary>
	/// used to identify part of sync message
	/// </summary>
	public enum Sync
	{
		SYNC_CHARACTER_POINTS=0x01,       // D charId, D charPoints, D corrPoints
		SYNC_CHARACTER_ATTRIBUTE=0x02,       // D charId, D attrId, DF base, DF mod
		SYNC_CHARACTER_SKILL=0x03,       // D charId, B skillId, D skill value
		SYNC_ONLINE_STATUS=0x04        // D charId, B 0 = offline, 1 = online
	}
}
