using System.Collections.Generic;
using ItemDeclutter.Definitions;
using UnityEngine;

namespace ItemDeclutter.Managers
{
	public static class PositionManager
	{
		public static Dictionary<string, Vector3> ItemCoordinates = [];

		// public static Dictionary<string, float> DroppedItemsYCoordinateDictionary = [];

		public static void AddItem(string itemName, Vector3 coordinates)
		{
			ItemCoordinates.Add(itemName, coordinates);
		}

		// public static void RemoveItem(string itemName){
		//     ItemCoordinates.Remove(itemName);
		// }

		public static void SetDictionaryFromSyncedData(ItemZoneData[] itemZoneData)
		{
			ItemCoordinates.Clear();
			foreach (var itemZone in itemZoneData)
			{
				ItemCoordinates.Add(itemZone.ObjectName, itemZone.Coordinates);
			}
		}

		public static Vector3 GetItemCoordinates(string itemName)
		{
			return ItemCoordinates.GetValueOrDefault(itemName);
		}

		public static Vector3 GetItemCoordinated(GrabbableObject item)
		{
			return GetItemCoordinates(item.itemProperties.itemName);
		}
	}
}
