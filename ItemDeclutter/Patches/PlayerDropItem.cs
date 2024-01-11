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

        if (resolvedCoordinates == null)
        {
          return;
        }

        Vector3 definedPosition = resolvedCoordinates;

        Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z} - updating drop position to {definedPosition.x}, {definedPosition.y}, {definedPosition.z}");

        targetFloorPosition = definedPosition;

        if (Positions.DroppedItemsYCoordinateDictionary.ContainsKey(itemName))
        {
          targetFloorPosition.y = Positions.DroppedItemsYCoordinateDictionary[itemName];
          Plugin.logger.LogInfo($"Updating {itemName} Y coordinate dictionary to {targetFloorPosition.y}");
        }

        // consistent item rotation
        floorYRot = 0;

        dropObject.customGrabTooltip = ItemStackTooltip.UpdateAllTooltips(itemName);

      }

    }


  }

}