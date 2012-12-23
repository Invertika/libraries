//
//  Account.cs
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
using System.Diagnostics;

namespace ISL.Server.Account
{
    public class Account
    {
        //        Account(const Account &rhs);
        //Account &operator=(const Account &rhs);

        string mName;        /**< User name */
        string mPassword;    /**< User password (hashed with salt) */
        string mRandomSalt;  /**< A random sequence sent to client to
                                       protect against replay attacks.*/
        string mEmail;       /**< User email address (hashed) */
        Dictionary<uint, Character> mCharacters;   /**< Character data */
        int mID;                  /**< Unique id */
        byte mLevel;     /**< Account level */
        DateTime mRegistrationDate; /**< Date and time of the account registration */
        DateTime mLastLogin;        /**< Date and time of the last login */

        public Account(int id=-1)
        {
            mID=id;
            mCharacters=new Dictionary<uint, Character>();
        }

        /// <summary>
        /// Get all the characters.
        /// </summary>
        /// <returns>all the characters.</returns>
        public Dictionary<uint, Character> getCharacters()
        {
            return mCharacters;
        }

        public bool isSlotEmpty(uint slot)
        {
            return !mCharacters.ContainsKey(slot);
        }

        public void setCharacters(Dictionary<uint, Character> characters)
        {
            mCharacters=characters;
        }

        public void addCharacter(Character character)
        {
            uint slot=character.getCharacterSlot();
            mCharacters[slot]=character;
        }

        void delCharacter(uint slot)
        {
            mCharacters.Remove(slot);
        }

        void setID(int id)
        {
            Debug.Assert(mID<0);
            mID=id;
        }

        //void setRegistrationDate(time_ ttime)
        //{
        //    //mRegistrationDate = time;
        //}

        public void setLastLogin(DateTime time)
        {
            mLastLogin=time;
        }

        /**
		 * Get the account level.
		 *
		 * @return the account level.
		 */
        public int getLevel()
        {
            return mLevel;
        }

        /**
   * Get account ID.
   *
   * @return the unique ID of the account, a negative number if none yet.
   */
        public int getID()
        {
            return mID;
        }

        /**
		 * Get the user name.
		 *
		 * @return the user name.
		 */
        public string getName()
        {
            return mName;
        }

        /**
		 * Get the user password (hashed with salt).
		 *
		 * @return the user password (hashed with salt).
		 */
        public string getPassword()
        {
            return mPassword;
        }

        /**
		 * Get the user random salt.
		 *
		 * @return the random salt used for next login.
		 */
        public string getRandomSalt()
        {
            return mRandomSalt;
        }

        /**
		 * Get the time of the last login.
		 */
        public DateTime getLastLogin()
        {
            return mLastLogin;
        }

        /**
		 * Set the user name.
		 *
		 * @param name the user name.
		 */
        public void setName(string name)
        {
            mName=name;
        }

        /**
		 * Set the user password. The password is expected to be already
		 * hashed with a salt.
		 *
		 * The hashing must be performed externally from this class or else
		 * we would end up with the password being hashed many times
		 * (e.g setPassword(getPassword()) would hash the password twice.
		 *
		 * @param password the user password (hashed with salt).
		 */
        public void setPassword(string password)
        {
            mPassword=password;
        }

        /**
		 * Set the user email address. The email address is expected to be
		 * already hashed.
		 *
		 * @param email the user email address (hashed).
		 */
        public void setEmail(string email)
        {
            mEmail=email;
        }

        /**
		 * Set the account level.
		 *
		 * @param level the new level.
		 */
        public void setLevel(int level)
        {
            mLevel=(byte)level;
        }

        public void setRegistrationDate(DateTime time)
        {
            mRegistrationDate=time;
        }

        /**
		 * Get the user email address (hashed).
		 *
		 * @return the user email address (hashed).
		 */
        public string getEmail()
        {
            return mEmail;
        }

        /**
		 * Get the time of the account registration.
		 */
        public DateTime getRegistrationDate()
        {
            return mRegistrationDate;
        }

        /**
		 * Set the random salt. This salt is sent to the client, so the client
		 * can hash its password with this random salt.
		 * This will help to protect against replay attacks.
		 *
		 * @param the new random salt to be sent out next login
		 */
        public void setRandomSalt(string salt)
        {
            mRandomSalt=salt;
        }
    }
}
