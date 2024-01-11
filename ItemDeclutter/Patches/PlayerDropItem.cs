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

    internal static bool PlayerOnShip()
    {
      return false;
    }

    internal static void DropIntoDesignatedPositionLogic(string dictionaryName)
    {

    }

    public static string SetDroppedItemsTooltip(string itemName)
    {
      GameObject ship = GameObject.Find("/Environment/HangarShip");

      var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>().Where(obj => obj.itemProperties.itemName == itemName);
      var count = ItemsOnShip.Count();

      Plugin.logger.LogInfo($"Found {count} {itemName} on ship");

      string tooltip = $"{itemName} (x{count}) \n Grab : [E]";

      foreach (var item in ItemsOnShip)
      {
        item.customGrabTooltip = tooltip;
      }

      return tooltip;
    }


    [HarmonyPatch("SetObjectAsNoLongerHeld")]
    [HarmonyPrefix]
    internal static void ItemDroppedOnShipPatch(bool droppedInElevator, bool droppedInShipRoom, ref Vector3 targetFloorPosition, ref GrabbableObject dropObject, ref int floorYRot)
    {
      if (droppedInShipRoom)
      {
        string itemName = dropObject.itemProperties.itemName;

        Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z}");

        // all defined positions
        if (Positions.PositionsDictionary.ContainsKey(itemName))
        {
          Vector3 definedPosition = Positions.PositionsDictionary[itemName];

          Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z} - updating drop position to {definedPosition.x}, {definedPosition.y}, {definedPosition.z}");

          targetFloorPosition = definedPosition;
          targetFloorPosition.y = Positions.DroppedItemsYCoordinateDictionary[itemName];

          // targetFloorPosition.x = definedPosition.x;
          // targetFloorPosition.y = definedPosition.y;
          // targetFloorPosition.z = definedPosition.z;

          // consistent item rotation
          floorYRot = 0;


          dropObject.customGrabTooltip = SetDroppedItemsTooltip(itemName);
        }
        else if (dropObject.scrapValue >= 10)
        {
          Vector3 position = Positions.PositionsDictionary["ScrapItemValue"];

          Plugin.logger.LogInfo($"Dropped {itemName} in ship room at {targetFloorPosition.x}, {targetFloorPosition.y}, {targetFloorPosition.z} - updating drop position to {position.x}, {position.y}, {position.z}");

          targetFloorPosition.x = position.x;
          // targetFloorPosition.y = position.y;
          targetFloorPosition.z = position.z;

          // consistent item rotation
          floorYRot = 0;
        }

      }



    }


  }

}