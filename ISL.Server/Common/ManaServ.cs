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

		/**
		 * Enumerated type for communicated messages:
		 *
		 * - PAMSG_*: from client to account server
		 * - APMSG_*: from account server to client
		 * - PCMSG_*: from client to chat server
		 * - CPMSG_*: from chat server to client
		 * - PGMSG_*: from client to game server
		 * - GPMSG_*: from game server to client
		 * - GAMSG_*: from game server to account server
		 *
		 * Components: B byte, W word, D double word, S variable-size string
		 *             C tile-based coordinates (B*3)
		 *
		 * Hosts:      P (player's client), A (account server), C (char server),
		 *             G (game server)
		 *
		 * TODO - Document specific error codes for each packet
		 */

		// Login/Register
		public const int PAMSG_REGISTER=0x0000; // D version, S username, S password, S email, S captcha response
		public const int APMSG_REGISTER_RESPONSE=0x0002; // B error, S updatehost, S Client data URL, B Character slots
		public const int PAMSG_UNREGISTER=0x0003; // S username, S password
		public const int APMSG_UNREGISTER_RESPONSE=0x0004; // B error
		public const int PAMSG_REQUEST_REGISTER_INFO=0x0005; //
		public const int APMSG_REGISTER_INFO_RESPONSE=0x0006; // B byte registration Allowed, byte minNameLength, byte maxNameLength, string captchaURL, string captchaInstructions
		public const int PAMSG_LOGIN=0x0010; // D version, S username, S password
		public const int APMSG_LOGIN_RESPONSE=0x0012; // B error, S updatehost, S Client data URL, B Character slots
		public const int PAMSG_LOGOUT=0x0013; // -
		public const int APMSG_LOGOUT_RESPONSE=0x0014; // B error
		public const int PAMSG_LOGIN_RNDTRGR=0x0015; // S username
		public const int APMSG_LOGIN_RNDTRGR_RESPONSE=0x0016; // S random seed
		public const int PAMSG_CHAR_CREATE=0x0020; // S name, B hair style, B hair color, B gender, B slot, {W stats}*
		public const int APMSG_CHAR_CREATE_RESPONSE=0x0021; // B error
		public const int PAMSG_CHAR_DELETE=0x0022; // B slot
		public const int APMSG_CHAR_DELETE_RESPONSE=0x0023; // B error
		// B slot, S name, B gender, B hair style, B hair color, W level,
		// W character points, W correction points,
		// {D attr id, D base value (in 1/256ths) D mod value (in 256ths) }*
		public const int APMSG_CHAR_INFO=0x0024; // ^
		public const int PAMSG_CHAR_SELECT=0x0026; // B slot
		public const int APMSG_CHAR_SELECT_RESPONSE=0x0027; // B error, B*32 token, S game address, W game port, S chat address, W chat port
		public const int PAMSG_EMAIL_CHANGE=0x0030; // S email
		public const int APMSG_EMAIL_CHANGE_RESPONSE=0x0031; // B error
		public const int PAMSG_PASSWORD_CHANGE=0x0034; // S old password, S new password
		public const int APMSG_PASSWORD_CHANGE_RESPONSE=0x0035; // B error

		public const int PGMSG_CONNECT=0x0050; // B*32 token
		public const int GPMSG_CONNECT_RESPONSE=0x0051; // B error
		public const int PCMSG_CONNECT=0x0053; // B*32 token
		public const int CPMSG_CONNECT_RESPONSE=0x0054; // B error

		public const int PGMSG_DISCONNECT=0x0060; // B reconnect account
		public const int GPMSG_DISCONNECT_RESPONSE=0x0061; // B error, B*32 token
		public const int PCMSG_DISCONNECT=0x0063; // -
		public const int CPMSG_DISCONNECT_RESPONSE=0x0064; // B error

		public const int PAMSG_RECONNECT=0x0065; // B*32 token
		public const int APMSG_RECONNECT_RESPONSE=0x0066; // B error

		// Game
		public const int GPMSG_PLAYER_MAP_CHANGE=0x0100; // S filename, W x, W y
		public const int GPMSG_PLAYER_SERVER_CHANGE=0x0101; // B*32 token, S game address, W game port
		public const int PGMSG_PICKUP=0x0110; // W*2 position
		public const int PGMSG_DROP=0x0111; // B slot, B amount
		public const int PGMSG_EQUIP=0x0112; // W inventory slot
		public const int PGMSG_UNEQUIP=0x0113; // W item Instance id
		public const int PGMSG_MOVE_ITEM=0x0114; // W slot1, W slot2, W amount
		public const int GPMSG_INVENTORY=0x0120; // { W slot, W item id [, W amount] (if item id is nonzero) }*
		public const int GPMSG_INVENTORY_FULL=0x0121; // W inventory slot count { W slot, W itemId, W amount }, { W equip slot, W item Id, W item Instance}*
		public const int GPMSG_EQUIP=0x0122; // W item Id, W equip slot type count //{ W equip slot, W capacity used}*//<- When equipping, //{ W item instance, W 0}*//<- When unequipping
		public const int GPMSG_PLAYER_ATTRIBUTE_CHANGE=0x0130; // { W attribute, D base value (in 1/256ths), D modified value (in 1/256ths)}*
		public const int GPMSG_PLAYER_EXP_CHANGE=0x0140; // { W skill, D exp got, D exp needed }*
		public const int GPMSG_LEVELUP=0x0150; // W new level, W character points, W correction points
		public const int GPMSG_LEVEL_PROGRESS=0x0151; // B percent completed to next levelup
		public const int PGMSG_RAISE_ATTRIBUTE=0x0160; // W attribute
		public const int GPMSG_RAISE_ATTRIBUTE_RESPONSE=0x0161; // B error, W attribute
		public const int PGMSG_LOWER_ATTRIBUTE=0x0170; // W attribute
		public const int GPMSG_LOWER_ATTRIBUTE_RESPONSE=0x0171; // B error, W attribute
		public const int PGMSG_RESPAWN=0x0180; // -
		public const int GPMSG_BEING_ENTER=0x0200; // B type, W being id, B action, W*2 position, B direction
		// character: S name, B hair style, B hair color, B gender, B sprite layers changed, { B slot type, W item id }*
		// monster: W type id gender
		// npc: W type id gender
		public const int GPMSG_BEING_LEAVE=0x0201; // W being id
		public const int GPMSG_ITEM_APPEAR=0x0202; // W item id, W*2 position
		public const int GPMSG_BEING_LOOKS_CHANGE=0x0210; // B sprite layers changed, { B slot type, W item id }*
		public const int PGMSG_WALK=0x0260; // W*2 destination
		public const int PGMSG_ACTION_CHANGE=0x0270; // B Action
		public const int GPMSG_BEING_ACTION_CHANGE=0x0271; // W being id, B action
		public const int PGMSG_DIRECTION_CHANGE=0x0272; // B Direction
		public const int GPMSG_BEING_DIR_CHANGE=0x0273; // W being id, B direction
		public const int GPMSG_BEING_HEALTH_CHANGE=0x0274; // W being id, W hp, W max hp
		public const int GPMSG_BEINGS_MOVE=0x0280; // { W being id, B flags [, [W*2 position,] W*2 destination, B speed] }*
		public const int GPMSG_ITEMS=0x0281; // { W item id, W*2 position }*
		public const int PGMSG_ATTACK=0x0290; // W being id
		public const int GPMSG_BEING_ATTACK=0x0291; // W being id, B direction, B attack Id
		public const int PGMSG_USE_SPECIAL=0x0292; // B specialID
		public const int GPMSG_SPECIAL_STATUS=0x0293; // { B specialID, D current, D max, D recharge }
		public const int PGMSG_SAY=0x02A0; // S text
		public const int GPMSG_SAY=0x02A1; // W being id, S text
		public const int GPMSG_NPC_CHOICE=0x02B0; // W being id, { S text }*
		public const int GPMSG_NPC_MESSAGE=0x02B1; // W being id, B* text
		public const int PGMSG_NPC_TALK=0x02B2; // W being id
		public const int PGMSG_NPC_TALK_NEXT=0x02B3; // W being id
		public const int PGMSG_NPC_SELECT=0x02B4; // W being id, B choice
		public const int GPMSG_NPC_BUY=0x02B5; // W being id, { W item id, W amount, W cost }*
		public const int GPMSG_NPC_SELL=0x02B6; // W being id, { W item id, W amount, W cost }*
		public const int PGMSG_NPC_BUYSELL=0x02B7; // W item id, W amount
		public const int GPMSG_NPC_ERROR=0x02B8; // B error
		public const int GPMSG_NPC_CLOSE=0x02B9; // W being id
		public const int GPMSG_NPC_POST=0x02D0; // W being id
		public const int PGMSG_NPC_POST_SEND=0x02D1; // W being id, { S name, S text, W item id }
		public const int GPMSG_NPC_POST_GET=0x02D2; // W being id, { S name, S text, W item id }
		public const int PGMSG_NPC_NUMBER=0x02D3; // W being id, D number
		public const int PGMSG_NPC_STRING=0x02D4; // W being id, S string
		public const int GPMSG_NPC_NUMBER=0x02D5; // W being id, D max, D min, D default
		public const int GPMSG_NPC_STRING=0x02D6; // W being id
		public const int PGMSG_TRADE_REQUEST=0x02C0; // W being id
		public const int GPMSG_TRADE_REQUEST=0x02C1; // W being id
		public const int GPMSG_TRADE_START=0x02C2; // -
		public const int GPMSG_TRADE_COMPLETE=0x02C3; // -
		public const int PGMSG_TRADE_CANCEL=0x02C4; // -
		public const int GPMSG_TRADE_CANCEL=0x02C5; // -
		public const int PGMSG_TRADE_AGREED=0x02C6; // -
		public const int GPMSG_TRADE_AGREED=0x02C7; // -
		public const int PGMSG_TRADE_CONFIRM=0x02C8; // -
		public const int GPMSG_TRADE_CONFIRM=0x02C9; // -
		public const int PGMSG_TRADE_ADD_ITEM=0x02CA; // B slot, B amount
		public const int GPMSG_TRADE_ADD_ITEM=0x02CB; // W item id, B amount
		public const int PGMSG_TRADE_SET_MONEY=0x02CC; // D amount
		public const int GPMSG_TRADE_SET_MONEY=0x02CD; // D amount
		public const int GPMSG_TRADE_BOTH_CONFIRM=0x02CE; // -
		public const int PGMSG_USE_ITEM=0x0300; // B slot
		public const int GPMSG_USE_RESPONSE=0x0301; // B error
		public const int GPMSG_BEINGS_DAMAGE=0x0310; // { W being id, W amount }*
		public const int GPMSG_CREATE_EFFECT_POS=0x0320; // W effect id, W*2 position
		public const int GPMSG_CREATE_EFFECT_BEING=0x0321; // W effect id, W BeingID
		public const int GPMSG_SHAKE=0x0330; // W intensityX, W intensityY, [W decay_times_10000, [W duration]]

		// Guild
		public const int PCMSG_GUILD_CREATE=0x0350; // S name
		public const int CPMSG_GUILD_CREATE_RESPONSE=0x0351; // B error, W guild, B rights, W channel
		public const int PCMSG_GUILD_INVITE=0x0352; // W id, S name
		public const int CPMSG_GUILD_INVITE_RESPONSE=0x0353; // B error
		public const int PCMSG_GUILD_ACCEPT=0x0354; // W id
		public const int CPMSG_GUILD_ACCEPT_RESPONSE=0x0355; // B error, W guild, B rights, W channel
		public const int PCMSG_GUILD_GET_MEMBERS=0x0356; // W id
		public const int CPMSG_GUILD_GET_MEMBERS_RESPONSE=0x0357; // S names, B online
		public const int CPMSG_GUILD_UPDATE_LIST=0x0358; // W id, S name, B event
		public const int PCMSG_GUILD_QUIT=0x0360; // W id
		public const int CPMSG_GUILD_QUIT_RESPONSE=0x0361; // B error
		public const int PCMSG_GUILD_PROMOTE_MEMBER=0x0365; // W guild, S name, B rights
		public const int CPMSG_GUILD_PROMOTE_MEMBER_RESPONSE=0x0366; // B error
		public const int PCMSG_GUILD_KICK_MEMBER=0x0370; // W guild, S name
		public const int CPMSG_GUILD_KICK_MEMBER_RESPONSE=0x0371; // B error

		public const int CPMSG_GUILD_INVITED=0x0388; // S char name, S  guild name, W id
		public const int CPMSG_GUILD_REJOIN=0x0389; // S name, W guild, W rights, W channel, S announce

		// Party
		public const int PGMSG_PARTY_INVITE=0x03A0; // S name
		public const int GPMSG_PARTY_INVITE_ERROR=0x03A1; // S name
		public const int GCMSG_PARTY_INVITE=0x03A2; // S inviter, S invitee
		public const int CPMSG_PARTY_INVITED=0x03A4; // S name
		public const int PCMSG_PARTY_INVITE_ANSWER=0x03A5; // S name, B accept
		public const int CPMSG_PARTY_INVITE_ANSWER_RESPONSE=0x03A6; // B error, { S name }
		public const int CPMSG_PARTY_REJECTED=0x03A8; // S name, B error
		public const int PCMSG_PARTY_QUIT=0x03AA; // -
		public const int CPMSG_PARTY_QUIT_RESPONSE=0x03AB; // B error
		public const int CPMSG_PARTY_NEW_MEMBER=0x03B0; // S name, S inviter
		public const int CPMSG_PARTY_MEMBER_LEFT=0x03B1; // D character id

		// Chat
		public const int CPMSG_ERROR=0x0401; // B error
		public const int CPMSG_ANNOUNCEMENT=0x0402; // S text
		public const int CPMSG_PRIVMSG=0x0403; // S user, S text
		public const int CPMSG_PUBMSG=0x0404; // W channel, S user, S text
		public const int PCMSG_CHAT=0x0410; // S text, W channel
		public const int PCMSG_ANNOUNCE=0x0411; // S text
		public const int PCMSG_PRIVMSG=0x0412; // S user, S text
		public const int PCMSG_WHO=0x0415; // -
		public const int CPMSG_WHO_RESPONSE=0x0416; // { S user }

		// -- Channeling
		public const int CPMSG_CHANNEL_EVENT=0x0430; // W channel, B event, S info
		public const int PCMSG_ENTER_CHANNEL=0x0440; // S channel, S password
		public const int CPMSG_ENTER_CHANNEL_RESPONSE=0x0441; // B error, W id, S name, S topic, S userlist
		public const int PCMSG_QUIT_CHANNEL=0x0443; // W channel id
		public const int CPMSG_QUIT_CHANNEL_RESPONSE=0x0444; // B error, W channel id
		public const int PCMSG_LIST_CHANNELS=0x0445; // -
		public const int CPMSG_LIST_CHANNELS_RESPONSE=0x0446; // S names, W number of users
		public const int PCMSG_LIST_CHANNELUSERS=0x0460; // S channel
		public const int CPMSG_LIST_CHANNELUSERS_RESPONSE=0x0461; // S channel, { S user, B mode }
		public const int PCMSG_TOPIC_CHANGE=0x0462; // W channel id, S topic
		// -- User modes
		public const int PCMSG_USER_MODE=0x0465; // W channel id, S name, B mode
		public const int PCMSG_KICK_USER=0x0466; // W channel id, S name

		// Inter-server
		public const int GAMSG_REGISTER=0x0500; // S address, W port, S password, D items db revision, { W map id }*
		public const int AGMSG_REGISTER_RESPONSE=0x0501; // C item version, C password response, { S globalvar_key, S globalvar_value }
		public const int AGMSG_ACTIVE_MAP=0x0502; // W map id, W Number of mapvar_key mapvar_value sent, { S mapvar_key, S mapvar_value }, W Number of map items, { D item Id, W amount, W posX, W posY }
		public const int AGMSG_PLAYER_ENTER=0x0510; // B*32 token, D id, S name, serialised character data
		public const int GAMSG_PLAYER_DATA=0x0520; // D id, serialised character data
		public const int GAMSG_REDIRECT=0x0530; // D id
		public const int AGMSG_REDIRECT_RESPONSE=0x0531; // D id, B*32 token, S game address, W game port
		public const int GAMSG_PLAYER_RECONNECT=0x0532; // D id, B*32 token
		public const int GAMSG_PLAYER_SYNC=0x0533; // serialised sync data
		public const int GAMSG_SET_VAR_CHR=0x0540; // D id, S name, S value
		public const int GAMSG_GET_VAR_CHR=0x0541; // D id, S name
		public const int AGMSG_GET_VAR_CHR_RESPONSE=0x0542; // D id, S name, S value
		//reserved GAMSG_SET_VAR_ACC           = 0x0543, // D charid, S name, S value
		//reserved GAMSG_GET_VAR_ACC           = 0x0544, // D charid, S name
		//reserved AGMSG_GET_VAR_ACC_RESPONSE  = 0x0545, // D charid, S name, S value
		public const int GAMSG_SET_VAR_MAP=0x0546; // D mapid, S name, S value
		public const int GAMSG_SET_VAR_WORLD=0x0547; // S name, S value
		public const int AGMSG_SET_VAR_WORLD=0x0548; // S name, S value
		public const int GAMSG_BAN_PLAYER=0x0550; // D id, W duration
		public const int GAMSG_CHANGE_PLAYER_LEVEL=0x0555; // D id, W level
		public const int GAMSG_CHANGE_ACCOUNT_LEVEL=0x0556; // D id, W level
		public const int GAMSG_STATISTICS=0x0560; // { W map id, W thing nb, W monster nb, W player nb, { D character id }* }*
		public const int CGMSG_CHANGED_PARTY=0x0590; // D character id, D party id
		public const int GCMSG_REQUEST_POST=0x05A0; // D character id
		public const int CGMSG_POST_RESPONSE=0x05A1; // D receiver id, { S sender name, S letter, W num attachments { W attachment item id, W quantity } }
		public const int GCMSG_STORE_POST=0x05A5; // D sender id, S receiver name, S letter, { W attachment item id, W quantity }
		public const int CGMSG_STORE_POST_RESPONSE=0x05A6; // D id, B error
		public const int GAMSG_TRANSACTION=0x0600; // D character id, D action, S message
		public const int GAMSG_CREATE_ITEM_ON_MAP=0x0601; // D map id, D item id, W amount, W pos x, W pos y
		public const int GAMSG_REMOVE_ITEM_ON_MAP=0x0602; // D map id, D item id, W amount, W pos x, W pos y

		public const int XXMSG_INVALID=0x7FFF;


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
