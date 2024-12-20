using System.Collections.Generic;
using System.Linq;
using ItemDeclutter.Definitions;
using UnityEngine;

namespace ItemDeclutter.Managers
{
	public static class PositionManager
	{
		public static Dictionary<string, Vector3> ItemCoordinates = [];

		// public static Dictionary<string, float> DroppedItemsYCoordinateDictionary = [];

		public static void Reset()
		{
			ItemCoordinates.Clear();
		}

		public static void AddItem(string itemName, Vector3 coordinates)
		{
			ItemCoordinates.Add(itemName, coordinates);
		}

		public static void SetDictionaryFromSyncedData(ItemZoneData[] itemZoneData)
		{
			ItemCoordinates.Clear();
			foreach (var itemZone in itemZoneData)
			{
				Vector3 itemPosition = new(itemZone.XCoordinate, itemZone.YCoordinate, itemZone.ZCoordinate);
				ItemCoordinates.Add(itemZone.ObjectName.ToString(), itemPosition);
			}
		}

		public static Vector3 GetItemCoordinates(string itemName)
		{
			return ItemCoordinates.GetValueOrDefault(itemName);
		}

		public static Vector3 GetItemCoordinates(GrabbableObject item)
		{
			return GetItemCoordinates(item.itemProperties.itemName);
		}

		public static bool IsItemDefined(string itemName)
		{
			return ItemCoordinates.ContainsKey(itemName);
		}

		public static void SetDictionaryOnHost()
		{
			if (!StartOfRound.Instance.IsHost)
			{
				return;
			}

			foreach (ItemConfig item in ItemManager.Grabbables)
			{
				var itemZone = item.Zone;

				if (itemZone == null || itemZone == "")
				{
					continue;
				}

				if (!ZoneManager.Zones.ContainsKey(itemZone))
				{
					Plugin.logger.LogMessage($"Zone {itemZone} does not exist - skipping.");
					continue;
				}

				// if (Positions.PositionsDictionary.ContainsKey(item.Key))
				// {
				// 	Plugin.logger.LogMessage($"{item.Key} is already in the dictionary - changing default position to configured.");

				// 	Positions.PositionsDictionary.Remove(item.Key);
				// 	Positions.ZoneAllocated.Remove(itemZone);
				// }

				var ItemCoordinates = ZoneManager.GetZoneCoordinates(itemZone);

				Plugin.logger.LogMessage($"itemZone: {itemZone}, coordinates: {ItemCoordinates}");

				// Positions.PositionsDictionary.Add(item.Key, itemPosition);
				// Positions.ZoneAllocated.Add(itemZone, true);

				PositionManager.AddItem(item.ObjectName, ItemCoordinates);

				Plugin.logger.LogMessage(
					$"Added {item.ObjectName} to PositionsDictionary with position {item.XCoordinate}, {item.YCoordinate}, {item.ZCoordinate}."
				);
			}

			if (DeclutterSync.Instance == null)
			{
				Plugin.logger.LogError("DeclutterSync instance is null");
				return;
			}

			DeclutterSync.Instance.SetNewOnHost(ItemCoordinates);
		}
	}
}
