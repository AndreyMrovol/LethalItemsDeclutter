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

    public static Dictionary<string, Vector3> Zones = new()
    {
      // {"001", vectorConstrctor(-6, 0.25, -5)},
      // {"002", vectorConstrctor(-5.65, 0.25, -5)},
      // {"003", vectorConstrctor(-5.3, 0.25, -5)},
      // {"004", vectorConstrctor(-4.95, 0.25, -5)},
      // {"005", vectorConstrctor(-4.6, 0.25, -5)},
      // {"006", vectorConstrctor(-4.25, 0.25, -5)},
      // {"007", vectorConstrctor(-3.9, 0.25, -5)},
      // {"008", vectorConstrctor(-3.55, 0.25, -5)},
      // {"009", vectorConstrctor(-3.2, 0.25, -5)},
      // {"010", vectorConstrctor(-2.85, 0.25, -5)},
      // {"011", vectorConstrctor(-2.5, 0.25, -5)},
      // {"012", vectorConstrctor(-2.15, 0.25, -5)},
      // {"013", vectorConstrctor(-1.8, 0.25, -5)},
      // {"014", vectorConstrctor(-1.45, 0.25, -5)},
      // {"015", vectorConstrctor(-1.1, 0.25, -5)},
      // {"016", vectorConstrctor(-0.75, 0.25, -5)},
      // {"017", vectorConstrctor(-0.4, 0.25, -5)},
      // {"018", vectorConstrctor(-0.05, 0.25, -5)},
      // {"019", vectorConstrctor(0.3, 0.25, -5)},
      // {"020", vectorConstrctor(0.65, 0.25, -5)},
      // {"021", vectorConstrctor(1, 0.25, -5)},
      // {"022", vectorConstrctor(1.35, 0.25, -5)},
      // {"023", vectorConstrctor(1.7, 0.25, -5)},
      // {"024", vectorConstrctor(2.05, 0.25, -5)},
      // {"025", vectorConstrctor(2.4, 0.25, -5)},
      // {"026", vectorConstrctor(2.75, 0.25, -5)},
      // {"027", vectorConstrctor(3.1, 0.25, -5)},
      // {"028", vectorConstrctor(3.45, 0.25, -5)},
      // {"029", vectorConstrctor(3.8, 0.25, -5)},
      // {"030", vectorConstrctor(4.15, 0.25, -5)},
      // {"031", vectorConstrctor(4.5, 0.25, -5)},
      // {"032", vectorConstrctor(4.85, 0.25, -5)},
      // {"033", vectorConstrctor(5.2, 0.25, -5)},
      // {"034", vectorConstrctor(5.55, 0.25, -5)},
      // {"035", vectorConstrctor(5.9, 0.25, -5)},

      // {"101", vectorConstrctor(-6, 0.25, -6)},
      // {"102", vectorConstrctor(-5.65, 0.25, -6)},
      // {"103", vectorConstrctor(-5.3, 0.25, -6)},
      // {"104", vectorConstrctor(-4.95, 0.25, -6)},
      // {"105", vectorConstrctor(-4.6, 0.25, -6)},
      // {"106", vectorConstrctor(-4.25, 0.25, -6)},
      // {"107", vectorConstrctor(-3.9, 0.25, -6)},
      // {"108", vectorConstrctor(-3.55, 0.25, -6)},
      // {"109", vectorConstrctor(-3.2, 0.25, -6)},
      // {"110", vectorConstrctor(-2.85, 0.25, -6)},
      // {"111", vectorConstrctor(-2.5, 0.25, -6)},
      // {"112", vectorConstrctor(-2.15, 0.25, -6)},
      // {"113", vectorConstrctor(-1.8, 0.25, -6)},
      // {"114", vectorConstrctor(-1.45, 0.25, -6)},
      // {"115", vectorConstrctor(-1.1, 0.25, -6)},
      // {"116", vectorConstrctor(-0.75, 0.25, -6)},
      // {"117", vectorConstrctor(-0.4, 0.25, -6)},
      // {"118", vectorConstrctor(-0.05, 0.25, -6)},
      // {"119", vectorConstrctor(0.3, 0.25, -6)},
      // {"120", vectorConstrctor(0.65, 0.25, -6)},
      // {"121", vectorConstrctor(1, 0.25, -6)},
      // {"122", vectorConstrctor(1.35, 0.25, -6)},
      // {"123", vectorConstrctor(1.7, 0.25, -6)},
      // {"124", vectorConstrctor(2.05, 0.25, -6)},
      // {"125", vectorConstrctor(2.4, 0.25, -6)},
      //       {"126", vectorConstrctor(2.75, 0.25, -6)},
      // {"127", vectorConstrctor(3.1, 0.25, -6)},
      // {"128", vectorConstrctor(3.45, 0.25, -6)},
      // {"129", vectorConstrctor(3.8, 0.25, -6)},
      // {"130", vectorConstrctor(4.15, 0.25, -6)},
      // {"131", vectorConstrctor(4.5, 0.25, -6)},
      // {"132", vectorConstrctor(4.85, 0.25, -6)},
      // {"133", vectorConstrctor(5.2, 0.25, -6)},
      // {"134", vectorConstrctor(5.55, 0.25, -6)},
      // {"135", vectorConstrctor(5.9, 0.25, -6)},

      // {"201", vectorConstrctor(-6, 0.25, -7)},
      // {"202", vectorConstrctor(-5.65, 0.25, -7)},
      // {"203", vectorConstrctor(-5.3, 0.25, -7)},
      // {"204", vectorConstrctor(-4.95, 0.25, -7)},
      // {"205", vectorConstrctor(-4.6, 0.25, -7)},
      // {"206", vectorConstrctor(-4.25, 0.25, -7)},
      // {"207", vectorConstrctor(-3.9, 0.25, -7)},
      // {"208", vectorConstrctor(-3.55, 0.25, -7)},
      // {"209", vectorConstrctor(-3.2, 0.25, -7)},
      // {"210", vectorConstrctor(-2.85, 0.25, -7)},
      // {"211", vectorConstrctor(-2.5, 0.25, -7)},
      // {"212", vectorConstrctor(-2.15, 0.25, -7)},
      // {"213", vectorConstrctor(-1.8, 0.25, -7)},
      // {"214", vectorConstrctor(-1.45, 0.25, -7)},
      // {"215", vectorConstrctor(-1.1, 0.25, -7)},
      // {"216", vectorConstrctor(-0.75, 0.25, -7)},
      // {"217", vectorConstrctor(-0.4, 0.25, -7)},
      // {"218", vectorConstrctor(-0.05, 0.25, -7)},
      // {"219", vectorConstrctor(0.3, 0.25, -7)},
      // {"220", vectorConstrctor(0.65, 0.25, -7)},
      // {"221", vectorConstrctor(1, 0.25, -7)},
      // {"222", vectorConstrctor(1.35, 0.25, -7)},
      // {"223", vectorConstrctor(1.7, 0.25, -7)},
      // {"224", vectorConstrctor(2.05, 0.25, -7)},
      // {"225",  vectorConstrctor(2.4, 0.25, -7)},
      //       {"226", vectorConstrctor(2.75, 0.25, -7)},
      // {"227", vectorConstrctor(3.1, 0.25, -7)},
      // {"228", vectorConstrctor(3.45, 0.25, -7)},
      // {"229", vectorConstrctor(3.8, 0.25, -7)},
      // {"230", vectorConstrctor(4.15, 0.25, -7)},
      // {"231", vectorConstrctor(4.5, 0.25, -7)},
      // {"232", vectorConstrctor(4.85, 0.25, -7)},
      // {"233", vectorConstrctor(5.2, 0.25, -7)},
      // {"234", vectorConstrctor(5.55, 0.25, -7)},
      // {"235", vectorConstrctor(5.9, 0.25, -7)},

      // {"301", vectorConstrctor(-6, 0.25, -8)},
      // {"302", vectorConstrctor(-5.65, 0.25, -8)},
      // {"303", vectorConstrctor(-5.3, 0.25, -8)},
      // {"304", vectorConstrctor(-4.95, 0.25, -8)},
      // {"305", vectorConstrctor(-4.6, 0.25, -8)},
      // {"306", vectorConstrctor(-4.25, 0.25, -8)},
      // {"307", vectorConstrctor(-3.9, 0.25, -8)},
      // {"308", vectorConstrctor(-3.55, 0.25, -8)},
      // {"309", vectorConstrctor(-3.2, 0.25, -8)},
      // {"310", vectorConstrctor(-2.85, 0.25, -8)},
      // {"311", vectorConstrctor(-2.5, 0.25, -8)},
      // {"312", vectorConstrctor(-2.15, 0.25, -8)},
      // {"313", vectorConstrctor(-1.8, 0.25, -8)},
      // {"314", vectorConstrctor(-1.45, 0.25, -8)},
      // {"315", vectorConstrctor(-1.1, 0.25, -8)},
      // {"316", vectorConstrctor(-0.75, 0.25, -8)},
      // {"317", vectorConstrctor(-0.4, 0.25, -8)},
      // {"318", vectorConstrctor(-0.05, 0.25, -8)},
      // {"319", vectorConstrctor(0.3, 0.25, -8)},
      // {"320", vectorConstrctor(0.65, 0.25, -8)},
      // {"321", vectorConstrctor(1, 0.25, -8)},
      // {"322", vectorConstrctor(1.35, 0.25, -8)},
      // {"323", vectorConstrctor(1.7, 0.25, -8)},
      // {"324", vectorConstrctor(2.05, 0.25, -8)},
      // {"325",  vectorConstrctor(2.4, 0.25, -8)},
      //       {"326", vectorConstrctor(2.75, 0.25, -8)},
      // {"327", vectorConstrctor(3.1, 0.25, -8)},
      // {"328", vectorConstrctor(3.45, 0.25, -8)},
      // {"329", vectorConstrctor(3.8, 0.25, -8)},
      // {"330", vectorConstrctor(4.15, 0.25, -8)},
      // {"331", vectorConstrctor(4.5, 0.25, -8)},
      // {"332", vectorConstrctor(4.85, 0.25, -8)},
      // {"333", vectorConstrctor(5.2, 0.25, -8)},
      // {"334", vectorConstrctor(5.55, 0.25, -8)},
      // {"335", vectorConstrctor(5.9, 0.25, -8)},

    };

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


  }

}