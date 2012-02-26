using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISL.Server.Utilities;

namespace ISL.Server.Common
{
	public class CommandLineOptions
	{
		static int DEFAULT_SERVER_PORT=9601;

		public CommandLineOptions()
		{
			verbosity=LogLevel.Warning;
			verbosityChanged=false;
			port=DEFAULT_SERVER_PORT;
			portChanged=false;
		}

		public string configPath;

		public LogLevel verbosity;
		public bool verbosityChanged;

		public int port;
		public bool portChanged;
	}
}
