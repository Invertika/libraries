using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Enums
{
	public enum ExitValue
	{
		EXIT_NORMAL=0,
		EXIT_CONFIG_NOT_FOUND, // The main configuration file wasn't found.
		EXIT_BAD_CONFIG_PARAMETER, // The configuration file has a wrong parameter.
		EXIT_XML_NOT_FOUND, // A required base xml configuration file wasn't found.
		EXIT_XML_BAD_PARAMETER, // The configuration of an xml file is faulty.
		EXIT_MAP_FILE_NOT_FOUND, // No map files found.
		EXIT_DB_EXCEPTION, // The database is invalid or unreachable.
		EXIT_NET_EXCEPTION, // The server was unable to start network connections.
		EXIT_OTHER_EXCEPTION
	}
}
