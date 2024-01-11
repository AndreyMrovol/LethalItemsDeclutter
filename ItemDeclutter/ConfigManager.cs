using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace ItemDeclutter;

public class ConfigManager
{
  public static ConfigManager Instance { get; private set; }

  public static void Init(ConfigFile config)
  {
    Instance = new ConfigManager(config);
  }

  // public static ConfigEntry<bool> Debug { get; private set; }

  public static ConfigEntry<bool> DivideCheapExpensive { get; private set; }
  public static ConfigEntry<int> ExpensiveValue { get; private set; }
  public static ConfigEntry<bool> DivideOneTwoHanded { get; private set; }
  public static ConfigEntry<bool> ShouldItemsAssumeZones { get; private set; }

  private ConfigManager(ConfigFile config)
  {
    // Debug = config.Bind("Debug", "Debugging", true, "Enable debug logging.");

    DivideCheapExpensive = config.Bind("Item Zones", "DivideCheapExpensive", true, "Divide cheap and expensive items into different zones.");
    ExpensiveValue = config.Bind("Item Zones", "ExpensiveValue", 65, "At what point value is considered expensive?");
    DivideOneTwoHanded = config.Bind("Item Zones", "DivideOneTwoHanded", true, "Divide normal and two handed items into different zones.");
    ShouldItemsAssumeZones = config.Bind("Item Zones", "ShouldItemsAssumeZones", true, "Should items assume zones if they are not defined in the config?");
  }
}