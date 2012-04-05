using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	/**
 * Guild member permissions
 * Members with NONE cannot invite users or set permissions
 * Members with TOPIC_CHANGE can change the guild channel topic
 * Members with INVITE can invite other users
 * Memeber with KICK can remove other users
 * Members with OWNER can invite users and set permissions
 */
	public enum GuildAccessLevel
	{
		GAL_NONE=0,
		GAL_TOPIC_CHANGE=1,
		GAL_INVITE=2,
		GAL_KICK=4,
		GAL_OWNER=255
	}
}
