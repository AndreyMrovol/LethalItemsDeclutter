using BepInEx.Configuration;

namespace ItemDeclutter;

public class ZoneManagerConfig
{
  public static ConfigEntry<bool> LogCreatedZones { get; private set; }

  public static ConfigEntry<float> ZoningStartX { get; private set; }
  public static ConfigEntry<float> ZoningStartY { get; private set; }
  public static ConfigEntry<float> ZoningStartZ { get; private set; }

  public static ConfigEntry<float> ZoneDistanceX { get; private set; }
  public static ConfigEntry<float> ZoneDistanceY { get; private set; }
  public static ConfigEntry<float> ZoneDistanceZ { get; private set; }

  public static ConfigEntry<int> HowManyZonesX { get; private set; }
  public static ConfigEntry<int> HowManyZonesY { get; private set; }
  public static ConfigEntry<int> HowManyZonesZ { get; private set; }

  public static ZoneManagerConfig Instance { get; private set; }

  public static void Init(ConfigFile config)
  {
    Instance = new ZoneManagerConfig(config);
  }

  public static ConfigEntry<bool> Debug { get; private set; }

  private ZoneManagerConfig(ConfigFile config)
  {
    LogCreatedZones = config.Bind("Log created zones", "Log", true, "Log created zones to console.");

    ZoningStartX = config.Bind("Start Position", "ZoningStartX", -6f, "Start position on X axis (entrance -> console)");
    ZoningStartY = config.Bind("Start Position", "ZoningStartY", 0.25f, "Start position on Y axis (floor -> ceiling)");
    ZoningStartZ = config.Bind("Start Position", "ZoningStartZ", -5f, "Start position on Z axis (left -> right)");

    ZoneDistanceX = config.Bind("Distance between zones", "ZoneDistanceX", 0.65f, "Distance between zones on X axis (entrance -> console)");
    ZoneDistanceY = config.Bind("Distance between zones", "ZoneDistanceY", 1.25f, "Distance between zones on Y axis (floor -> ceiling)");
    ZoneDistanceZ = config.Bind("Distance between zones", "ZoneDistanceZ", 1f, "Distance between zones on Z axis (left -> right)");

    HowManyZonesX = config.Bind("How many zones to create", "HowManyZonesX", 20, "How many zones on X axis (entrance -> console)");
    HowManyZonesY = config.Bind("How many zones to create", "HowManyZonesY", 5, "How many zones on Y axis (floor -> ceiling)");
    HowManyZonesZ = config.Bind("How many zones to create", "HowManyZonesZ", 4, "How many zones on Z axis (left -> right)");
  }
}