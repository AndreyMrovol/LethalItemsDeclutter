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

  [HarmonyPatch(typeof(GrabbableObject))]
  internal class FallToGroundPatch
  {

    [HarmonyPatch("FallToGround")]
    [HarmonyPrefix]
    internal static bool FallToGroundPatchLogic(GrabbableObject __instance, ref bool randomizePosition)
    {
      // Harmony bool patching - returning true stops prefix and executes the original, returning false stops prefix and skips the original

      Plugin.logger.LogDebug($"FallToGroundPatchLogic called for {__instance.itemProperties.itemName}.");


      if (__instance == null) return true;
      if (__instance.transform.parent == null) return true;
      // if (__instance.customGrabTooltip != null) return true;

      Plugin.logger.LogDebug($"transformparent: {__instance.transform.parent}.");
      Plugin.logger.LogDebug($"parent: {__instance.parentObject}.");


      if (__instance.transform.parent.name == "HangarShip")
      {

        var resolvedZone = ItemZone.GetName(__instance);
        Plugin.logger.LogDebug($"Resolved zone for {__instance.itemProperties.itemName} to {resolvedZone}");
        if (resolvedZone == null) return true;

        if (resolvedZone == true.ToString())
        {
          Plugin.logger.LogDebug($"{__instance.itemProperties.itemName} is in the dictionary - zone {resolvedZone}");
        }
        else
        {
          Plugin.logger.LogDebug($"{__instance.itemProperties.itemName} is not in the dictionary - checking if {resolvedZone} is a valid zone name.");


          if (Positions.Zones.ContainsKey(resolvedZone))
          {
            Plugin.logger.LogDebug($"{resolvedZone} is a valid zone name - adding {__instance.itemProperties.itemName} to the dictionary.");
            Positions.PositionsDictionary[__instance.itemProperties.itemName] = Positions.Zones[resolvedZone];
          }
          else
          {
            Plugin.logger.LogDebug($"{resolvedZone} is not a valid zone name.");
            return true;
          }
        }

        if (ZoneManagerConfig.ZoningStartY.Value != __instance.transform.localPosition.y)
        {
          Plugin.logger.LogDebug($"{__instance.itemProperties.itemName}'s position is not on the floor.");
          __instance.targetFloorPosition = Positions.PositionsDictionary[__instance.itemProperties.itemName];
        }
        else
        {
          return true;
        }


        return false;
      }
      else
      {
        Plugin.logger.LogMessage($"{__instance.itemProperties.itemName} is not on the ship. - calling original method.");
        return true;
      }



    }


  }


}
