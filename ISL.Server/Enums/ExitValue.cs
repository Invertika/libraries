//
//  ExitValue.cs
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
	public enum ExitValue
	{
		EXIT_NORMAL=0,
		EXIT_CONFIG_NOT_FOUND, // The main configuration file wasn't found.
		EXIT_BAD_CONFIG_PARAMETER, // The configuration file has a wrong parameter.
		EXIT_XML_NOT_FOUND, // A required base xml configuration file wasn't found.
		EXIT_XML_BAD_PARAMETER, // The configuration of an xml file is faulty.
		EXIT_MAP_FILE_NOT_FOUND, // No map files found.
		EXIT_DB_EXCEPTION, // The database is invalid or unreachable.
		EXIT_NET_EXCEPTION, // The server was unable to start network connections.
		EXIT_OTHER_EXCEPTION
	}
}
