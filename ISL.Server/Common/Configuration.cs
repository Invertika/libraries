using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCL;
using System.Xml;

namespace ISL.Server.Common
{
	public static class Configuration
	{
		public static string Filename { get; private set; }

		public const int DEFAULT_SERVER_PORT=9601;

		const string DEFAULT_CONFIG_FILE="manaserv.xml";

		static XmlData xmlfile;
		static List<XmlNode> nodes;

		public static void Init(string filename)
		{
			if(filename==null||filename=="")
			{
				Filename=DEFAULT_CONFIG_FILE;
			}
			else
			{
				Filename=filename;
			}

			if(!FileSystem.ExistsFile(Filename)) throw new Exception();

			xmlfile=new XmlData(Filename);
			nodes=xmlfile.GetElements("configuration.option");
		}

		public static void deinitialize()
		{
		}

		public static string getValue(string key, string deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					return node.Attributes["value"].Value;
				}
			}

			return deflt;
		}

		public static int getValue(string key, int deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					return Convert.ToInt32(node.Attributes["value"].Value);
				}
			}

			return deflt;
		}

		public static bool getBoolValue(string key, bool deflt)
		{
			foreach(XmlNode node in nodes)
			{
				if(node.Attributes["name"].Value==key)
				{
					return Convert.ToBoolean(node.Attributes["value"].Value);
				}
			}

			return deflt;
		}
	}
}