using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Login
	{
		// Login specific return values
		LOGIN_INVALID_VERSION=0x40,       // the user is using an incompatible protocol
		LOGIN_INVALID_TIME=0x50,       // the user tried logging in too fast
		LOGIN_BANNED=0x51       // the user is currently banned
	}
}
