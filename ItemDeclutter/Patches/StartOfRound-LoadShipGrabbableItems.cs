using System.Linq;
using HarmonyLib;
using ItemDeclutter.Definitions;
using ItemDeclutter.Managers;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	internal class ShipLoadItems
	{
		[HarmonyPatch("LoadShipGrabbableItems")]
		[HarmonyPostfix]
		internal static void LoadShipItemsPatch()
		{
			GameObject ship = GameObject.Find("/Environment/HangarShip");
			var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>();

			foreach (var item in PositionManager.ItemCoordinates)
			{
				var itemsOfType = ItemsOnShip.Where(obj => obj.itemProperties.itemName == item.Key);

				Plugin.logger.LogDebug($"Found {itemsOfType.Count()} items of type {item.Key}");
				if (itemsOfType.Count() == 0)
				{
					continue;
				}

				foreach (GrabbableObject itemOfType in itemsOfType)
				{
					Vector3 moveItemToPosition = item.Value;

					// change position to predefined one
					itemOfType.transform.localPosition = moveItemToPosition;

					itemOfType.isInShipRoom = true;

					// if (ZoneManagerConfig.ZoningStartY.Value == moveItemToPosition.y)
					// {
					// 	itemOfType.FallToGround();
					// 	Positions.DroppedItemsYCoordinateDictionary[item.Key] = itemOfType.targetFloorPosition.y;
					// 	Plugin.logger.LogDebug(
					// 		$"{item.Key}'s position is in the lowest zone (on the floor) - updating Y coordinate dictionary to {itemOfType.targetFloorPosition.y}"
					// 	);
					// }
				}

				TooltipHelper.UpdateAllTooltips(item.Key);
			}
		}
	}
}
