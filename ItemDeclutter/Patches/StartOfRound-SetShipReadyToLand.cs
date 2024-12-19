using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	class SetShipReadyToLand
	{
		[HarmonyPatch("SetShipReadyToLand")]
		[HarmonyPrefix]
		internal static void PatchLogic()
		{
			Plugin.logger.LogInfo($"SetShipReadyToLand called");
			GameObject ship = GameObject.Find("/Environment/HangarShip");
			var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>();

			foreach (var item in Positions.PositionsDictionary)
			{
				var itemsOfType = ItemsOnShip.Where(obj => obj.itemProperties.itemName == item.Key);

				Plugin.logger.LogDebug($"Found {itemsOfType.Count()} items of type {item.Key}");
				if (itemsOfType.Count() == 0)
					continue;

				TooltipHelper.UpdateAllTooltips(item.Key);
			}
		}
	}
}
