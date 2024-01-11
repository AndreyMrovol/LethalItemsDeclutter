using System;
using System.Collections.Generic;
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

    public static Dictionary<string, Vector3> PositionsDictionary = new()
    {
      // {"ScrapItemValue", Zones["A306"]},

      // {"Radar-booster", Zones["A001"]},
      // {"Extension ladder", Zones["B004"]},

      //   {"Flashlight", Zones["A006"]},
      //   {"Pro-flashlight", Zones["A007"]},
      //   {"Walkie-talkie", Zones["A008"]},

      //   {"Shovel", Zones["A011"]},
      //   {"Stun grenade", Zones["A015"]},
      //   {"Zap gun", Zones["A017"]},


      //   {"Spray paint", Zones["A025"]},
    };

    public static Dictionary<string, string> DefaultZones = new(){
      {"Default", "A000"},
      {"ScrapCheap", "A302"},
      {"ScrapExpensive", "A306"},
      {"ScrapTwohandedCheap", "A311"},
      {"ScrapTwoHandedExpensive", "A318"}
    };


  }

}