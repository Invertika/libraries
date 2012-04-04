using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Create
	{
		// Character creation specific return values
		CREATE_INVALID_HAIRSTYLE=0x40,
		CREATE_INVALID_HAIRCOLOR=0x41,
		CREATE_INVALID_GENDER=0x42,
		CREATE_ATTRIBUTES_TOO_HIGH=0x43,
		CREATE_ATTRIBUTES_TOO_LOW=0x44,
		CREATE_ATTRIBUTES_OUT_OF_RANGE=0x45,
		CREATE_EXISTS_NAME=0x46,
		CREATE_TOO_MUCH_CHARACTERS=0x47,
		CREATE_INVALID_SLOT=0x48
	}
}
