using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using UnityEngine;

namespace ItemDeclutter
{

  public class Positions
  {
    // dictionary of positions and item names

    public static Vector3 vectorConstrctor(double x, double y, double z)
    {
      return new Vector3((float)x, (float)y, (float)z);
    }

    public static Dictionary<string, Vector3> Zones = new() { };

    public static Dictionary<string, Boolean> ZoneAllocated = new() { };

    public static Dictionary<string, float> DroppedItemsYCoordinateDictionary = new() { };

    public static Dictionary<string, Vector3> PositionsDictionary = new() { };

    public static Dictionary<string, string> DefaultZones = new(){
      {"Default", "A000"},

      {"ScrapCheap", "A302"},
      {"ScrapExpensive", "A306"},
      {"ScrapTwohandedCheap", "A311"},
      {"ScrapTwoHandedExpensive", "A318"},

      {"Shovel", "A000"},

      {"Pro-flashlight", "A002"},
      {"Walkie-talkie", "A003"},
      {"Flashlight", "A004"},

      {"Lockpicker", "A005"},

      {"Jetpack", "D301"},
      {"Extension ladder", "D001"},
    };


  }

}