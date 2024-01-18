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

  class ItemStackTooltip
  {

    public static string UpdateAllTooltips(string itemName)
    {
      GameObject ship = GameObject.Find("/Environment/HangarShip");

      var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>().Where(obj => obj.itemProperties.itemName == itemName).ToList();
      var ListClone = ItemsOnShip.ToList();

      if (ListClone.Count() == 0) return null;
      if (!Positions.PositionsDictionary.ContainsKey(itemName)) return null;

      ListClone.Do(item =>
        {
          if (item.playerHeldBy != null)
          {
            Plugin.logger.LogDebug($"Found a player holding {item.itemProperties.itemName}");
            ItemsOnShip.Remove(item);
          }
        }
      );


      var count = ItemsOnShip.Count();
      Plugin.logger.LogInfo($"Found {count} {itemName} on ship");

      string tooltip = $"{itemName} (x{count}) \n Grab : [E]";

      foreach (var item in ItemsOnShip)
      {
        item.customGrabTooltip = tooltip;
      }

      return tooltip;
    }

  }
}