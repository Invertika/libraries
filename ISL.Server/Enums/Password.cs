using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Password
	{
		// used in AGMSG_REGISTER_RESPNSE to show if password was accepted
		PASSWORD_OK=0x00,
		PASSWORD_BAD=0x01
	}
}
