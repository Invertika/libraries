using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum Moving
	{
		// Moving object flags
		// Payload contains the current position.
		MOVING_POSITION=1,
		// Payload contains the destination.
		MOVING_DESTINATION=2
	}
}
