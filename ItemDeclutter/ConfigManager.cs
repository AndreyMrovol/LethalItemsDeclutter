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

  public static ConfigEntry<bool> Debug { get; private set; }

  private ConfigManager(ConfigFile config)
  {
    Debug = config.Bind("Debug", "Debugging", true, "Enable debug logging.");
  }
}