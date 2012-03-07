using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Common
{
	// Character attribute modification specific return value
	public enum AttribmodResponseCode
	{
		ATTRIBMOD_OK=ManaServ.ERRMSG_OK,
		ATTRIBMOD_INVALID_ATTRIBUTE=0x40,
		ATTRIBMOD_NO_POINTS_LEFT=0x41,
		ATTRIBMOD_DENIED=0x42
	}
}
