using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum DataVersion
	{
		// used in AGMSG_REGISTER_RESPONSE to show state of item db
		DATA_VERSION_OK=0x00,
		DATA_VERSION_OUTDATED=0x01
	}
}
