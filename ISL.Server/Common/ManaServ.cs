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

		// Generic return values

		public const int ERRMSG_OK=0;                      // everything is fine
		public const int ERRMSG_FAILURE=1;                     // the action failed
		public const int ERRMSG_NO_LOGIN=2;                    // the user is not yet logged
		public const int ERRMSG_NO_CHARACTER_SELECTED=3;       // the user needs a character
		public const int ERRMSG_INSUFFICIENT_RIGHTS=4;         // the user is not privileged
		public const int ERRMSG_INVALID_ARGUMENT=5;            // part of the received message was invalid
		public const int ERRMSG_EMAIL_ALREADY_EXISTS=6;        // The Email Address already exists
		public const int ERRMSG_ALREADY_TAKEN=7;               // name used was already taken
		public const int ERRMSG_SERVER_FULL=8;                 // the server is overloaded
		public const int ERRMSG_TIME_OUT=9;                    // data failed to arrive in due time
		public const int ERRMSG_LIMIT_REACHED=10;               // limit reached
		public const int ERRMSG_ADMINISTRATIVE_LOGOFF=11;        // kicked by server administrator


		// used in AGMSG_REGISTER_RESPONSE to show state of item db
		public const int DATA_VERSION_OK=0x00;
		public const int DATA_VERSION_OUTDATED=0x01;


		// used in AGMSG_REGISTER_RESPNSE to show if password was accepted
		public const int PASSWORD_OK=0x00;
		public const int PASSWORD_BAD=0x01;

		// used to identify part of sync message
		public const int SYNC_CHARACTER_POINTS=0x01;       // D charId, D charPoints, D corrPoints
		public const int SYNC_CHARACTER_ATTRIBUTE=0x02;       // D charId, D attrId, DF base, DF mod
		public const int SYNC_CHARACTER_SKILL=0x03;       // D charId, B skillId, D skill value
		public const int SYNC_ONLINE_STATUS=0x04;       // D charId, B 0 = offline, 1 = online

		// Login specific return values
		public const int LOGIN_INVALID_VERSION=0x40;       // the user is using an incompatible protocol
		public const int LOGIN_INVALID_TIME=0x50;       // the user tried logging in too fast
		public const int LOGIN_BANNED=0x51;       // the user is currently banned

		// Account register specific return values
		public const int REGISTER_INVALID_VERSION=0x40;    // the user is using an incompatible protocol
		public const int REGISTER_EXISTS_USERNAME=0x41;         // there already is an account with this username
		public const int REGISTER_EXISTS_EMAIL=0x42;           // there already is an account with this email address
		public const int REGISTER_CAPTCHA_WRONG=0x43;          // user didn't solve the captcha correctly

		// Character creation specific return values
		public const int CREATE_INVALID_HAIRSTYLE=0x40;
		public const int CREATE_INVALID_HAIRCOLOR=0x41;
		public const int CREATE_INVALID_GENDER=0x42;
		public const int CREATE_ATTRIBUTES_TOO_HIGH=0x43;
		public const int CREATE_ATTRIBUTES_TOO_LOW=0x44;
		public const int CREATE_ATTRIBUTES_OUT_OF_RANGE=0x45;
		public const int CREATE_EXISTS_NAME=0x46;
		public const int CREATE_TOO_MUCH_CHARACTERS=0x47;
		public const int CREATE_INVALID_SLOT=0x48;

		// Moving object flags
		// Payload contains the current position.
		public const int MOVING_POSITION=1;
		// Payload contains the destination.
		public const int MOVING_DESTINATION=2;


		// Chat errors return values
		public const int CHAT_USING_BAD_WORDS=0x40;
		public const int CHAT_UNHANDLED_COMMAND=0x41;

		// Chat channels event values
		public const int CHAT_EVENT_NEW_PLAYER=0;
		public const int CHAT_EVENT_LEAVING_PLAYER=1;
		public const int CHAT_EVENT_TOPIC_CHANGE=2;
		public const int CHAT_EVENT_MODE_CHANGE=3;
		public const int CHAT_EVENT_KICKED_PLAYER=4;


		// Guild member event values
		public const int GUILD_EVENT_NEW_PLAYER=0;
		public const int GUILD_EVENT_LEAVING_PLAYER=1;
		public const int GUILD_EVENT_ONLINE_PLAYER=2;
		public const int GUILD_EVENT_OFFLINE_PLAYER=3;

		/**
		  * Moves enum for beings and actors for others players vision.
		  * WARNING: Has to be in sync with the same enum in the Being class
		  * of the client!
		  */
		enum BeingAction
		{
			STAND,
			WALK,
			ATTACK,
			SIT,
			DEAD,
			HURT
		}

		/**
		  * Moves enum for beings and actors for others players attack types.
		  * WARNING: Has to be in sync with the same enum in the Being class
		  * of the client!
		  */
		enum AttackType
		{
			HIT=0x00,
			CRITICAL=0x0a,
			MULTI=0x08,
			REFLECT=0x04,
			FLEE=0x0b
		}

		/**
		 * Beings and actors directions
		 * WARNING: Has to be in sync with the same enum in the Being class
		 * of the client!
		 */
		enum BeingDirection
		{
			DOWN=1,
			LEFT=2,
			UP=4,
			RIGHT=8
		}

		/**
		 * Beings Genders
		 */
		enum BeingGender
		{
			GENDER_MALE=0,
			GENDER_FEMALE,
			GENDER_UNSPECIFIED
		}

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
