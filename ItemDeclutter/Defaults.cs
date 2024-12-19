using System.Collections.Generic;

namespace ItemDeclutter
{
	public static class Defaults
	{
		public static Dictionary<string, string> DefaultZones =
			new()
			{
				{ "Default", "A000" },
				{ "Shovel", "A000" },
				{ "Stun grenade", "A002" },
				{ "Pro-flashlight", "A003" },
				{ "Flashlight", "A103" },
				{ "Walkie-talkie", "A004" },
				{ "Lockpicker", "A005" },
				{ "Key", "A105" },
				{ "Shotgun", "A006" },
				{ "Ammo", "A007" },
				{ "Spray paint", "A008" },
				{ "Zap gun", "A009" },
				{ "Jetpack", "D301" },
				{ "Extension ladder", "D001" },
			};

		public static string[] zoneYNames = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
	}
}
