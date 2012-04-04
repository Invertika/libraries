using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum ChatValues
	{
		// Chat errors return values
		CHAT_USING_BAD_WORDS=0x40,
		CHAT_UNHANDLED_COMMAND=0x41,

		// Chat channels event values
		CHAT_EVENT_NEW_PLAYER=0,
		CHAT_EVENT_LEAVING_PLAYER=1,
		CHAT_EVENT_TOPIC_CHANGE=2,
		CHAT_EVENT_MODE_CHANGE=3,
		CHAT_EVENT_KICKED_PLAYER=4
	}
}
