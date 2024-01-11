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
using UnityEngine.UIElements;
using UnityEngine.UIElements.Collections;

namespace ItemDeclutter
{

  [HarmonyPatch(typeof(StartOfRound))]
  internal class LoadIntoShipPatch
  {

    internal static Dictionary<string, ConfigEntry<string>> ItemZoneConfig = new Dictionary<string, ConfigEntry<string>>();

    [HarmonyPatch("LoadShipGrabbableItems")]
    [HarmonyPrefix]
    internal static void PatchLogic()
    {
      Positions.PositionsDictionary.Clear();

      CreateZones();
      PopulateItemScrapConfig();
      TranslateDictionaries();
    }

    internal static void PopulateItemScrapConfig()
    {
      List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

      foreach (Item item in allItemsList)
      {
        if (item.isScrap) continue;

        ItemZoneConfig[item.itemName] = Plugin.ItemZoneConfig.Bind(item.itemName, item.itemName, "", $"Set zone for {item.itemName}.");
        Plugin.logger.LogMessage($"Added config entry for {item.itemName}.");
        Plugin.logger.LogMessage($"ItemZoneConfig[item.itemName]: {ItemZoneConfig[item.itemName].Value}");
      }

      foreach (Item item in allItemsList)
      {
        if (!item.isScrap) continue;

        ItemZoneConfig[item.itemName] = Plugin.ScrapZoneConfig.Bind(item.itemName, item.itemName, "", $"Set zone for {item.itemName}.");
        Plugin.logger.LogMessage($"Added config entry for {item.itemName}.");
        Plugin.logger.LogMessage($"ItemZoneConfig[item.itemName]: {ItemZoneConfig[item.itemName].Value}");
      }
    }

    internal static void CreateZones()
    {
      Positions.Zones.Clear();

      for (int zoneY = 0; zoneY < ZoneManagerConfig.HowManyZonesY.Value; zoneY++)
      {
        for (int zoneX = 0; zoneX < ZoneManagerConfig.HowManyZonesX.Value; zoneX++)
        {
          for (int zoneZ = 0; zoneZ < ZoneManagerConfig.HowManyZonesZ.Value; zoneZ++)
          {
            var zoneName = $"{ZoneNames.zoneYNames[zoneY]}{zoneZ.ToString("0")}{zoneX.ToString("00")}";
            var zonePosition = new Vector3(
              ZoneManagerConfig.ZoningStartX.Value + (ZoneManagerConfig.ZoneDistanceX.Value * zoneX),
              ZoneManagerConfig.ZoningStartY.Value + (ZoneManagerConfig.ZoneDistanceY.Value * zoneY),
              ZoneManagerConfig.ZoningStartZ.Value + (ZoneManagerConfig.ZoneDistanceZ.Value * zoneZ * -1)
            );

            if (ZoneManagerConfig.LogCreatedZones.Value) Plugin.logger.LogInfo($"Created zone {zoneName} at {zonePosition.x}, {zonePosition.y}, {zonePosition.z}");

            Positions.Zones.Add(zoneName, zonePosition);
          }
        }
      }
    }

    internal static void TranslateDictionaries()
    {

      foreach (var item in ItemZoneConfig)
      {
        var itemZone = item.Value.Value;

        if (itemZone == null || itemZone == "") continue;

        var itemPosition = Positions.Zones[itemZone];

        Plugin.logger.LogMessage($"itemZone: {itemZone}, itemPosition: {itemPosition}");

        Positions.PositionsDictionary.Add(item.Key, itemPosition);
        Plugin.logger.LogMessage($"Added {item.Key} to PositionsDictionary with position {itemPosition.x}, {itemPosition.y}, {itemPosition.z}.");
      }


    }

  }
}