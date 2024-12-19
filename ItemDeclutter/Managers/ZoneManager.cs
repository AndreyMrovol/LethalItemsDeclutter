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
	}
}
