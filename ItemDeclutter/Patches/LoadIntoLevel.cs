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
    // internal static Dictionary<string, ConfigEntry<string>> ItemSizeConfig = new Dictionary<string, ConfigEntry<string>>();

    [HarmonyPatch("LoadShipGrabbableItems")]
    [HarmonyPrefix]
    internal static void PatchLogic()
    {
      Positions.PositionsDictionary.Clear();
      Positions.ZoneAllocated.Clear();
      Positions.Zones.Clear();

      CreateZones();
      PopulateItemScrapConfig();
      TranslateDictionaries();

      // if (ConfigManager.ShouldItemsAssumeZones.Value) AssumeZones();

    }

    internal static void PopulateItemScrapConfig()
    {
      List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

      foreach (Item item in allItemsList)
      {
        if (item.isScrap) continue;

        string defaultZone = "";

        if (Positions.DefaultZones.ContainsKey(item.itemName))
        {
          Plugin.logger.LogMessage($"{item.itemName} has a default zone : {Positions.DefaultZones[item.itemName]}");
          defaultZone = Positions.DefaultZones[item.itemName];
        }

        ItemZoneConfig[item.itemName] = Plugin.ItemZoneConfig.Bind(item.itemName, item.itemName, defaultZone, $"Set zone for {item.itemName}.");
        // ItemSizeConfig[item.itemName] = Plugin.ItemZoneConfig.Bind(item.itemName, "Size", "small", $"Set size of {item.itemName} (small/medium/large).");

        Plugin.logger.LogMessage($"Added config entry for {item.itemName}.");
        Plugin.logger.LogMessage($"ItemZoneConfig[item.itemName]: {ItemZoneConfig[item.itemName].Value}");
      }

      foreach (Item item in allItemsList)
      {
        if (!item.isScrap) continue;

        string defaultZone = "";

        if (Positions.DefaultZones.ContainsKey(item.itemName))
        {
          Plugin.logger.LogMessage($"{item.itemName} has a default zone : {Positions.DefaultZones[item.itemName]}");
          defaultZone = Positions.DefaultZones[item.itemName];
        }

        ItemZoneConfig[item.itemName] = Plugin.ScrapZoneConfig.Bind(item.itemName, item.itemName, defaultZone, $"Set zone for {item.itemName}.");
        // ItemSizeConfig[item.itemName] = Plugin.ScrapZoneConfig.Bind(item.itemName, "Size", "small", $"Set size of {item.itemName} (small/medium/large).");

        Plugin.logger.LogMessage($"Added config entry for {item.itemName}.");
        Plugin.logger.LogMessage($"ItemZoneConfig[item.itemName]: {ItemZoneConfig[item.itemName].Value}");
      }
    }

    internal static void CreateZones()
    {

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

      // foreach (var defaultZone in Positions.DefaultZones)
      // {
      //   var itemPosition = Positions.Zones[defaultZone.Value];

      //   if (defaultZone.Key == "Default") continue;

      //   if (!Positions.Zones.ContainsKey(defaultZone.Value))
      //   {
      //     Plugin.logger.LogMessage($"Zone {defaultZone.Value} does not exist - skipping.");
      //     continue;
      //   }

      //   Plugin.logger.LogMessage($"defaultZone: {defaultZone}, itemPosition: {itemPosition}");

      //   Positions.PositionsDictionary.Add(defaultZone.Key, itemPosition);
      //   Positions.ZoneAllocated.Add(defaultZone.Value, true);
      //   Plugin.logger.LogMessage($"Added {defaultZone.Key} to PositionsDictionary with position {itemPosition.x}, {itemPosition.y}, {itemPosition.z}.");
      // }

      foreach (var item in ItemZoneConfig)
      {
        var itemZone = item.Value.Value;

        if (itemZone == null || itemZone == "") continue;
        if (!Positions.Zones.ContainsKey(itemZone))
        {
          Plugin.logger.LogMessage($"Zone {itemZone} does not exist - skipping.");
          continue;
        }

        if (Positions.PositionsDictionary.ContainsKey(item.Key))
        {
          Plugin.logger.LogMessage($"{item.Key} is already in the dictionary - changing default position to configured.");

          Positions.PositionsDictionary.Remove(item.Key);
          Positions.ZoneAllocated.Remove(itemZone);
        }

        var itemPosition = Positions.Zones[itemZone];

        Plugin.logger.LogMessage($"itemZone: {itemZone}, itemPosition: {itemPosition}");

        Positions.PositionsDictionary.Add(item.Key, itemPosition);
        Positions.ZoneAllocated.Add(itemZone, true);
        Plugin.logger.LogMessage($"Added {item.Key} to PositionsDictionary with position {itemPosition.x}, {itemPosition.y}, {itemPosition.z}.");
      }

    }

    internal static void AssumeZones()
    {
      List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

      foreach (Item item in allItemsList)
      {
        if (item.isScrap) continue;

        if (Positions.PositionsDictionary.ContainsKey(item.itemName))
        {
          Plugin.logger.LogMessage($"{item.itemName} is already in the dictionary - skipping.");
          continue;

        }
        else
        {
          Plugin.logger.LogMessage($"{item.itemName} is not in the dictionary - finding a free zone.");
          FindFurthestFreeZone(item);
        }


      }
    }

    internal static void FindFurthestFreeZone(Item item)
    {
      var lastZoneX = ZoneManagerConfig.HowManyZonesX.Value - 1;
      var lastZoneZ = ZoneManagerConfig.HowManyZonesZ.Value - 1;

      // select all zones from the last (x,z) pair, check if they're allocated, if not allocate

      for (int zoneX = lastZoneX; zoneX >= 0; zoneX--)
      {
        for (int zoneZ = lastZoneZ; zoneZ >= 0; zoneZ--)
        {
          var zoneName = $"A{zoneZ.ToString("0")}{zoneX.ToString("00")}";
          var zonePosition = Positions.Zones[zoneName];

          if (Positions.ZoneAllocated.ContainsKey(zoneName)) continue;

          Positions.PositionsDictionary.Add(item.itemName, zonePosition);
          Positions.ZoneAllocated.Add(zoneName, true);
          Plugin.logger.LogMessage($"Auto-allocated {item.itemName} into zone {zoneName}.");

          return;
        }


      }
    }

  }
}