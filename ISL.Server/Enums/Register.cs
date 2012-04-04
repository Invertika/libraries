using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Register
	{
		// Account register specific return values
		REGISTER_INVALID_VERSION=0x40,   // the user is using an incompatible protocol
		REGISTER_EXISTS_USERNAME=0x41,         // there already is an account with this username
		REGISTER_EXISTS_EMAIL=0x42,           // there already is an account with this email address
		REGISTER_CAPTCHA_WRONG=0x43          // user didn't solve the captcha correctly
	}
}
