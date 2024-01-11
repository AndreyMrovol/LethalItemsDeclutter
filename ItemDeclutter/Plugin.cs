using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;

namespace ItemDeclutter
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource logger;
        private readonly Harmony harmony = new("ItemDeclutter");

        public static ConfigFile ItemZoneConfig = new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/Items.cfg"), true);
        public static ConfigFile ScrapZoneConfig = new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/Scrap.cfg"), true);

        private void Awake()
        {
            ConfigManager.Init(Config);
            ZoneManagerConfig.Init(new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/DefineZones.cfg"), true));
            // ItemZoneConfig.Init(new ConfigFile(Path.Combine(Paths.ConfigPath, "Declutter/Items.cfg"), true));

            Logger.LogWarning(StartOfRound.Instance == null);

            logger = Logger;
            harmony.PatchAll();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}