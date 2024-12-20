using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using ItemDeclutter.Definitions;
using ItemDeclutter.Managers;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(PlayerControllerB))]
	internal class PlayerDropItemPatch
	{
		[HarmonyPatch("SetObjectAsNoLongerHeld")]
		[HarmonyPrefix]
		internal static void ItemDroppedOnShipPatch(
			bool droppedInShipRoom,
			ref Vector3 targetFloorPosition,
			ref GrabbableObject dropObject,
			ref int floorYRot
		)
		{
			if (droppedInShipRoom)
			{
				GrabbableObject grabbableObject = dropObject;

				string itemName = grabbableObject.itemProperties.itemName;

				Plugin.logger.LogInfo(
					$"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z}"
				);

				Vector3 resolvedCoordinates = PositionManager.GetItemCoordinates(itemName);
				Vector3 originalCoordinates = targetFloorPosition;

				Vector3 definedPosition = resolvedCoordinates;

				if (resolvedCoordinates == new Vector3(0, 0, 0) || resolvedCoordinates == null)
				{
					Plugin.logger.LogInfo($"Coordinates for {itemName} are null");
					return;
				}

				if (resolvedCoordinates.y == ConfigManager.ZoningStartY.Value)
				{
					Plugin.logger.LogInfo($"{itemName}'s position is in the lowest zone - changing Y coordinate to {originalCoordinates.y}");
					definedPosition.y = originalCoordinates.y;
				}

				Plugin.logger.LogInfo(
					$"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z} - updating drop position to {definedPosition.x}, {definedPosition.y}, {definedPosition.z}"
				);

				targetFloorPosition = definedPosition;

				// if (ZoneManagerConfig.ZoningStartY.Value == targetFloorPosition.y)
				// {
				// 	Plugin.logger.LogDebug($"{dropObject.itemProperties.itemName}'s position is in the lowest zone - resolving Y coordinate");
				// 	bool isInYDictionary = Positions.DroppedItemsYCoordinateDictionary.ContainsKey(itemName);

				// 	if (isInYDictionary)
				// 	{
				// 		Plugin.logger.LogDebug(
				// 			$"{dropObject.itemProperties.itemName} is in the Y coordinate dictionary - updating Y coordinate to {Positions.DroppedItemsYCoordinateDictionary[itemName]}"
				// 		);
				// 		targetFloorPosition.y = Positions.DroppedItemsYCoordinateDictionary[itemName];
				// 	}
				// 	else
				// 	{
				// 		targetFloorPosition.y = originalCoordinates.y;
				// 		dropObject.FallToGround();
				// 		Positions.DroppedItemsYCoordinateDictionary[itemName] = originalCoordinates.y;
				// 		Plugin.logger.LogDebug(
				// 			$"{dropObject.itemProperties.itemName} is not in the Y coordinate dictionary - updating Y coordinate to {originalCoordinates.y} and adding to dictionary"
				// 		);
				// 		ConfigSync.UpdateItemPositionData(dropObject);
				// 	}
				// }

				// consistent item rotation
				floorYRot = 0;
			}
		}

		[HarmonyPatch(typeof(GrabbableObject))]
		[HarmonyPatch("DiscardItemClientRpc")]
		[HarmonyPostfix]
		internal static void ItemDroppedOnShipPatchPostfix(GrabbableObject __instance)
		{
			if (__instance.isInShipRoom)
			{
				string itemName = __instance.itemProperties.itemName;
				__instance.customGrabTooltip = TooltipHelper.GetTooltip(itemName);
			}
			else
			{
				__instance.customGrabTooltip = null;
			}
		}
	}
}
