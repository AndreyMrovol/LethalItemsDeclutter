using System.Collections.Generic;
using BepInEx.Configuration;
using HarmonyLib;
using ItemDeclutter.Managers;
using UnityEngine.UIElements;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	internal class LoadIntoShipPatch
	{
		[HarmonyPatch("LoadShipGrabbableItems")]
		[HarmonyPrefix]
		internal static void PatchLogic()
		{
			// Positions.PositionsDictionary.Clear();
			// Positions.ZoneAllocated.Clear();
			// Positions.Zones.Clear();



			if (StartOfRound.Instance.IsHost)
			{
				ZoneManager.CreateZones();
				ItemManager.PopulateItemScrapConfig();
				PositionManager.SetDictionaryOnHost();
			}

			// if (ConfigManager.ShouldItemsAssumeZones.Value) AssumeZones();

			// ConfigSync.SetNetworkedConfig();
		}

		// internal static void AssumeZones()
		// {
		// 	List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

		// 	foreach (Item item in allItemsList)
		// 	{
		// 		if (item.isScrap)
		// 			continue;

		// 		if (Positions.PositionsDictionary.ContainsKey(item.itemName))
		// 		{
		// 			Plugin.logger.LogMessage($"{item.itemName} is already in the dictionary - skipping.");
		// 			continue;
		// 		}
		// 		else
		// 		{
		// 			Plugin.logger.LogMessage($"{item.itemName} is not in the dictionary - finding a free zone.");
		// 			FindFurthestFreeZone(item);
		// 		}
		// 	}
		// }

		// internal static void FindFurthestFreeZone(Item item)
		// {
		// 	var lastZoneX = ZoneManagerConfig.HowManyZonesX.Value - 1;
		// 	var lastZoneZ = ZoneManagerConfig.HowManyZonesZ.Value - 1;

		// 	// select all zones from the last (x,z) pair, check if they're allocated, if not allocate

		// 	for (int zoneX = lastZoneX; zoneX >= 0; zoneX--)
		// 	{
		// 		for (int zoneZ = lastZoneZ; zoneZ >= 0; zoneZ--)
		// 		{
		// 			var zoneName = $"A{zoneZ.ToString("0")}{zoneX.ToString("00")}";
		// 			var zonePosition = Positions.Zones[zoneName];

		// 			if (Positions.ZoneAllocated.ContainsKey(zoneName))
		// 				continue;

		// 			Positions.PositionsDictionary.Add(item.itemName, zonePosition);
		// 			Positions.ZoneAllocated.Add(zoneName, true);
		// 			Plugin.logger.LogMessage($"Auto-allocated {item.itemName} into zone {zoneName}.");

		// 			return;
		// 		}
		// 	}
		// }
	}
}
