//
//  Point.cs
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

namespace ISL.Server.Utilities
{
	/**
	 * A point in positive space. Usually represents pixel coordinates on a map.
	 */
	public class Point
	{
		public Point()
		{
			x=0;
			y=0;
		}

		public Point(int X, int Y)
		{
			x=X;
			y=Y;
		}

		public int x; /**< x coordinate */
		public int y; /**< y coordinate */

		///**
		// * Check whether the given point is within range of this point.
		// */
		//bool inRangeOf(const Point &p, int radius) const
		//{
		//    return std::abs(x - p.x) <= radius &&
		//           std::abs(y - p.y) <= radius;
		//}

		//bool operator== (const Point &other) const
		//{
		//    return (x == other.x && y == other.y);
		//}

		//bool operator!= (const Point &other) const
		//{
		//    return (x != other.x || y != other.y);
		//}
	}
}
