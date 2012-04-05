//
//  ErrorMessage.cs
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
	public enum ErrorMessage
	{
		// Generic return values
		ERRMSG_OK=0,                      // everything is fine
		ERRMSG_FAILURE=1,                     // the action failed
		ERRMSG_NO_LOGIN=2,                    // the user is not yet logged
		ERRMSG_NO_CHARACTER_SELECTED=3,     // the user needs a character
		ERRMSG_INSUFFICIENT_RIGHTS=4,         // the user is not privileged
		ERRMSG_INVALID_ARGUMENT=5,          // part of the received message was invalid
		ERRMSG_EMAIL_ALREADY_EXISTS=6,       // The Email Address already exists
		ERRMSG_ALREADY_TAKEN=7,             // name used was already taken
		ERRMSG_SERVER_FULL=8,                // the server is overloaded
		ERRMSG_TIME_OUT=9,                   // data failed to arrive in due time
		ERRMSG_LIMIT_REACHED=10,            // limit reached
		ERRMSG_ADMINISTRATIVE_LOGOFF=11       // kicked by server administrator
	}
}
