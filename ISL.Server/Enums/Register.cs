//
//  Register.cs
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
	public enum Register
	{
		// Account register specific return values
		REGISTER_INVALID_VERSION=0x40,   // the user is using an incompatible protocol
		REGISTER_EXISTS_USERNAME=0x41,         // there already is an account with this username
		REGISTER_EXISTS_EMAIL=0x42,           // there already is an account with this email address
		REGISTER_CAPTCHA_WRONG=0x43          // user didn't solve the captcha correctly
	}
}
