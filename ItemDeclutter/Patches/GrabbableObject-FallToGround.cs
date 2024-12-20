using HarmonyLib;
using ItemDeclutter.Definitions;
using ItemDeclutter.Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(GrabbableObject))]
	internal class FallToGroundPatch
	{
		[HarmonyPatch("FallToGround")]
		[HarmonyPrefix]
		internal static bool FallToGroundPatchLogic(GrabbableObject __instance, ref bool randomizePosition)
		{
			// Harmony bool patching - returning true stops prefix and executes the original, returning false stops prefix and skips the original

			Plugin.debugLogger.LogDebug($"FallToGroundPatchLogic called for {__instance.itemProperties.itemName}.");

			if (__instance == null)
			{
				return true;
			}

			if (__instance.transform.parent == null)
			{
				return true;
			}

			Plugin.debugLogger.LogDebug($"Transform parent name: {__instance.transform.parent.name}");

			if (__instance.transform.parent.name == "HangarShip")
			{
				string itemName = __instance.itemProperties.itemName;
				Vector3 ItemCoordinates = PositionManager.GetItemCoordinates(itemName);

				if (ItemCoordinates == new Vector3(0, 0, 0) || ItemCoordinates == null)
				{
					Plugin.debugLogger.LogDebug($"Coordinates for {itemName} are null.");
					return true;
				}

				// var resolvedZone = itemConfig.Zone;
				// Plugin.debugLogger.LogDebug($"Resolved zone for {__instance.itemProperties.itemName} to {resolvedZone}");
				// if (resolvedZone == "")
				// {
				// 	return true;
				// }

				// if (resolvedZone == true.ToString())
				// {
				// 	Plugin.debugLogger.LogDebug($"{__instance.itemProperties.itemName} is in the dictionary - zone {resolvedZone}");
				// }
				// else
				// {
				// 	Plugin.debugLogger.LogDebug(
				// 		$"{__instance.itemProperties.itemName} is not in the dictionary - checking if {resolvedZone} is a valid zone name."
				// 	);

				// 	if (Positions.Zones.ContainsKey(resolvedZone))
				// 	{
				// 		Plugin.debugLogger.LogDebug(
				// 			$"{resolvedZone} is a valid zone name - adding {__instance.itemProperties.itemName} to the dictionary."
				// 		);
				// 		Positions.PositionsDictionary[__instance.itemProperties.itemName] = Positions.Zones[resolvedZone];
				// 	}
				// 	else
				// 	{
				// 		Plugin.debugLogger.LogDebug($"{resolvedZone} is not a valid zone name.");
				// 		return true;
				// 	}
				// }

				// if (ZoneManagerConfig.ZoningStartY.Value != __instance.transform.localPosition.y)
				// {
				// 	Plugin.debugLogger.LogDebug($"{__instance.itemProperties.itemName}'s position is not on the floor.");
				// 	__instance.targetFloorPosition = Positions.PositionsDictionary[__instance.itemProperties.itemName];
				// }
				// else
				// {
				// 	return true;
				// }

				return false;
			}
			else
			{
				Plugin.debugLogger.LogMessage($"{__instance.itemProperties.itemName} is not on the ship. - calling original method.");
				return true;
			}
		}
	}
}
