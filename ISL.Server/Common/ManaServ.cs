//
//  ManaServ.cs
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
	public static class ManaServ
	{
		public const int PROTOCOL_VERSION=1;
		public const int SUPPORTED_DB_VERSION=19;

		public const int WORLD_TICK_MS=100;

		public const int MAGIC_TOKEN_LENGTH=32; //TODO muss auf die Länge von Varios.GetUniqueID angepasst werden

		/**
 * Default tile length in pixel
 */
		public const int DEFAULT_TILE_LENGTH=32;

		// Helper functions for gender

		/**
		* Helper function for getting gender by int
		*/
		static BeingGender getGender(int gender)
		{
			switch(gender)
			{
				case 0:
					return BeingGender.GENDER_MALE;
				case 1:
					return BeingGender.GENDER_FEMALE;
				default:
					return BeingGender.GENDER_UNSPECIFIED;
			}
		}

		/**
		* Helper function for getting gender by string
		*/
		static BeingGender getGender(string gender)
		{
			if(gender.ToLower()=="male") return BeingGender.GENDER_MALE;
			else if(gender.ToLower()=="female") return BeingGender.GENDER_FEMALE;
			else return BeingGender.GENDER_UNSPECIFIED;
		}
	}
}
