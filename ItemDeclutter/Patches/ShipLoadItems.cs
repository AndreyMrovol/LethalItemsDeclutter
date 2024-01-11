using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter
{

  [HarmonyPatch(typeof(StartOfRound))]
  internal class ItemStackTooltipPatch
  {

    [HarmonyPatch("LoadShipGrabbableItems")]
    [HarmonyPostfix]
    internal static void LoadShipItemsPatch()
    {
      GameObject ship = GameObject.Find("/Environment/HangarShip");
      var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>();

      foreach (var item in Positions.PositionsDictionary)
      {
        var itemsOfType = ItemsOnShip.Where(obj => obj.itemProperties.itemName == item.Key);

        foreach (var itemOfType in itemsOfType)
        {
          Vector3 moveItemToPosition = item.Value;

          // change position to predefined one
          itemOfType.transform.localPosition = moveItemToPosition;

          itemOfType.isInShipRoom = true;

          // Plugin.logger.LogMessage($"Trying to move {item.Key} to {itemOfType.transform.localPosition.x}, {itemOfType.transform.localPosition.y}, {itemOfType.transform.localPosition.z}");
          itemOfType.FallToGround();

          Positions.DroppedItemsYCoordinateDictionary[item.Key] = itemOfType.targetFloorPosition.y;
          // Plugin.logger.LogMessage($"Updating {item.Key} Y coordinate dictionary to {itemOfType.targetFloorPosition.y}");
          // Plugin.logger.LogDebug($"Dictionary checkup: {Positions.DroppedItemsYCoordinateDictionary[item.Key]}");

          // Plugin.logger.LogDebug($"Floor position: {itemOfType.targetFloorPosition.x}, {itemOfType.targetFloorPosition.y}, {itemOfType.targetFloorPosition.z}");
        }

        ItemStackTooltip.UpdateAllTooltips(item.Key);
      }

    }


  }

}