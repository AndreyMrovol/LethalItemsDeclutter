using System.Collections.Generic;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace ItemDeclutter.Managers
{
	public class ZoneManager
	{
		public static Dictionary<string, Vector3> Zones = [];

		public static Dictionary<string, bool> ZoneAllocated = [];

		public static void CreateZone(string zoneName, Vector3 coordinates)
		{
			Zones.Add(zoneName, coordinates);
		}

		public static void RemoveZone(string zoneName)
		{
			Zones.Remove(zoneName);
		}

		public static Vector3 GetZoneCoordinates(string zoneName)
		{
			return Zones.GetValueOrDefault(zoneName);
		}

		public static bool IsZoneAllocated(string zoneName)
		{
			return ZoneAllocated.GetValueOrDefault(zoneName);
		}

		public static void SetZoneAllocated(string zoneName, bool isAllocated)
		{
			ZoneAllocated[zoneName] = isAllocated;
		}

		public static void SetZoneAllocated(string zoneName)
		{
			SetZoneAllocated(zoneName, true);
		}

		public static void Reset()
		{
			Zones.Clear();
			ZoneAllocated.Clear();
		}

		internal static void CreateZones()
		{
			if (!StartOfRound.Instance.IsHost)
			{
				return;
			}

			for (int zoneY = 0; zoneY < ConfigManager.HowManyZonesY.Value; zoneY++)
			{
				for (int zoneX = 0; zoneX < ConfigManager.HowManyZonesX.Value; zoneX++)
				{
					for (int zoneZ = 0; zoneZ < ConfigManager.HowManyZonesZ.Value; zoneZ++)
					{
						var zoneName = $"{Defaults.zoneYNames[zoneY]}{zoneZ.ToString("0")}{zoneX.ToString("00")}";
						var zonePosition = new UnityEngine.Vector3(
							ConfigManager.ZoningStartX.Value + (ConfigManager.ZoneDistanceX.Value * zoneX),
							ConfigManager.ZoningStartY.Value + (ConfigManager.ZoneDistanceY.Value * zoneY),
							ConfigManager.ZoningStartZ.Value + (ConfigManager.ZoneDistanceZ.Value * zoneZ * -1)
						);

						if (ConfigManager.LogCreatedZones.Value)
						{
							Plugin.logger.LogInfo($"Created zone {zoneName} at {zonePosition.x}, {zonePosition.y}, {zonePosition.z}");
						}

						Zones.Add(zoneName, zonePosition);
					}
				}
			}
		}
	}
}
