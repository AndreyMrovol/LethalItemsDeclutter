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

      {"Shovel", "A000"},

      {"Stun grenade", "A002"},

      {"Pro-flashlight", "A003"},
      {"Flashlight", "A103"},

      {"Walkie-talkie", "A004"},

      {"Lockpicker", "A005"},
      {"Key", "A105"},

      {"Shotgun", "A006"},
      {"Ammo", "A007"},

      {"Spray paint", "A008"},

      {"Zap gun", "A009"},

      {"Jetpack", "D301"},
      {"Extension ladder", "D001"},
    };


  }

}