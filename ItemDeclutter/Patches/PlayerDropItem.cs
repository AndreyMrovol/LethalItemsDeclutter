using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter
{

  [HarmonyPatch(typeof(PlayerControllerB))]
  internal class PlayerDropItemPatch
  {

    [HarmonyPatch("SetObjectAsNoLongerHeld")]
    [HarmonyPrefix]
    internal static void ItemDroppedOnShipPatch(bool droppedInElevator, bool droppedInShipRoom, ref Vector3 targetFloorPosition, ref GrabbableObject dropObject, ref int floorYRot)
    {
      if (droppedInShipRoom)
      {
        string itemName = dropObject.itemProperties.itemName;

        Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z}");

        if (!Positions.PositionsDictionary.ContainsKey(itemName)) return;

        var resolvedCoordinates = ItemZone.GetCoordinates(dropObject);
        var originalCoordinates = targetFloorPosition;

        if (resolvedCoordinates == null)
        {
          return;
        }

        Vector3 definedPosition = resolvedCoordinates;

        Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z} - updating drop position to {definedPosition.x}, {definedPosition.y}, {definedPosition.z}");

        targetFloorPosition = definedPosition;

        if (ZoneManagerConfig.ZoningStartY.Value == targetFloorPosition.y)
        {
          Plugin.logger.LogDebug($"{dropObject.itemProperties.itemName}'s position is in the lowest zone - resolving Y coordinate");
          bool isInYDictionary = Positions.DroppedItemsYCoordinateDictionary.ContainsKey(itemName);

          if (isInYDictionary)
          {
            Plugin.logger.LogDebug($"{dropObject.itemProperties.itemName} is in the Y coordinate dictionary - updating Y coordinate to {Positions.DroppedItemsYCoordinateDictionary[itemName]}");
            targetFloorPosition.y = Positions.DroppedItemsYCoordinateDictionary[itemName];
          }
          else
          {
            targetFloorPosition.y = originalCoordinates.y;
            dropObject.FallToGround();
            Positions.DroppedItemsYCoordinateDictionary[itemName] = originalCoordinates.y;
            Plugin.logger.LogDebug($"{dropObject.itemProperties.itemName} is not in the Y coordinate dictionary - updating Y coordinate to {originalCoordinates.y} and adding to dictionary");
          }

        }

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
        __instance.customGrabTooltip = ItemStackTooltip.UpdateAllTooltips(itemName);
      }
      else
      {
        __instance.customGrabTooltip = null;
      }
    }


  }

}