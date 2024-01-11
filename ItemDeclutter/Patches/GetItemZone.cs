using Unity;
using UnityEngine;

namespace ItemDeclutter
{

  public static class ItemZone
  {
    public static string GetName(GrabbableObject droppedObject)
    {

      if (Positions.PositionsDictionary.ContainsKey(droppedObject.itemProperties.itemName))
      {
        return true.ToString();
      }

      var isScrap = droppedObject.itemProperties.isScrap;
      var isTwoHanded = droppedObject.itemProperties.twoHanded;
      var itemValue = droppedObject.scrapValue;

      if (isScrap)
      {

        if (isTwoHanded && ConfigManager.DivideOneTwoHanded.Value)
        {
          if (itemValue >= ConfigManager.ExpensiveValue.Value && ConfigManager.DivideCheapExpensive.Value)
          {
            return Positions.DefaultZones["ScrapTwoHandedExpensive"];
          }
          else
          {
            return Positions.DefaultZones["ScrapTwohandedCheap"];

          }
        }
        else
        {
          if (itemValue >= ConfigManager.ExpensiveValue.Value && ConfigManager.DivideCheapExpensive.Value)
          {
            return Positions.DefaultZones["ScrapExpensive"];
          }
          else
          {
            return Positions.DefaultZones["ScrapCheap"];

          }
        }



      }



      // "not found - something went so horribly wrong it's not even funny";
      return null;
    }


    public static Vector3 GetCoordinates(GrabbableObject droppedObject)
    {
      var resolvedZone = GetName(droppedObject);

      if (resolvedZone == true.ToString())
      {
        return Positions.PositionsDictionary[droppedObject.itemProperties.itemName];
      }
      else if (Positions.Zones.ContainsKey(resolvedZone))
      {
        return Positions.Zones[resolvedZone];
      }

      return Positions.Zones["Default"];

    }

  }

}