using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ItemDeclutter
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		internal static ManualLogSource logger;
		internal static ManualLogSource debugLogger => logger;
		private readonly Harmony harmony = new("ItemDeclutter");

		// public static ConfigFile ItemZoneConfig = new(Path.Combine(Paths.ConfigPath, "Declutter/Items.cfg"), true);
		// public static ConfigFile ScrapZoneConfig = new(Path.Combine(Paths.ConfigPath, "Declutter/Scrap.cfg"), true);

		private void Awake()
		{
			ConfigManager.Init(new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/BaseConfig.cfg"), true));
			// ZoneManagerConfig.Init(new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/DefineZones.cfg"), true));

			logger = Logger;
			// debugLogger = new("Declutter");

			harmony.PatchAll();

			// Plugin startup logic
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
		}
	}
}
