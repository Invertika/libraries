//
//  Create.cs
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

namespace ISL.Server.Enums
{
	public enum Create
	{
		// Character creation specific return values
		CREATE_INVALID_HAIRSTYLE=0x40,
		CREATE_INVALID_HAIRCOLOR=0x41,
		CREATE_INVALID_GENDER=0x42,
		CREATE_ATTRIBUTES_TOO_HIGH=0x43,
		CREATE_ATTRIBUTES_TOO_LOW=0x44,
		CREATE_ATTRIBUTES_OUT_OF_RANGE=0x45,
		CREATE_EXISTS_NAME=0x46,
		CREATE_TOO_MUCH_CHARACTERS=0x47,
		CREATE_INVALID_SLOT=0x48
	}
}
