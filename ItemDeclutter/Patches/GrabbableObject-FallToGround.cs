using HarmonyLib;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(GrabbableObject))]
	internal class FallToGroundPatch
	{
		[HarmonyPatch("FallToGround")]
		[HarmonyPrefix]
		internal static bool FallToGroundPatchLogic(GrabbableObject __instance, ref bool randomizePosition)
		{
			// Harmony bool patching - returning true stops prefix and executes the original, returning false stops prefix and skips the original

			Plugin.DebugLogger.LogDebug($"FallToGroundPatchLogic called for {__instance.itemProperties.itemName}.");

			if (__instance == null)
			{
				return true;
			}
			if (__instance.transform.parent == null)
			{
				return true;
			}

			Plugin.DebugLogger.LogDebug($"Transform parent name: {__instance.transform.parent.name}");

			if (__instance.transform.parent.name == "HangarShip")
			{
				var resolvedZone = ItemZone.GetName(__instance);
				Plugin.DebugLogger.LogDebug($"Resolved zone for {__instance.itemProperties.itemName} to {resolvedZone}");
				if (resolvedZone == null)
				{
					return true;
				}

				if (resolvedZone == true.ToString())
				{
					Plugin.DebugLogger.LogDebug($"{__instance.itemProperties.itemName} is in the dictionary - zone {resolvedZone}");
				}
				else
				{
					Plugin.DebugLogger.LogDebug(
						$"{__instance.itemProperties.itemName} is not in the dictionary - checking if {resolvedZone} is a valid zone name."
					);

					if (Positions.Zones.ContainsKey(resolvedZone))
					{
						Plugin.DebugLogger.LogDebug(
							$"{resolvedZone} is a valid zone name - adding {__instance.itemProperties.itemName} to the dictionary."
						);
						Positions.PositionsDictionary[__instance.itemProperties.itemName] = Positions.Zones[resolvedZone];
					}
					else
					{
						Plugin.DebugLogger.LogDebug($"{resolvedZone} is not a valid zone name.");
						return true;
					}
				}

				if (ZoneManagerConfig.ZoningStartY.Value != __instance.transform.localPosition.y)
				{
					Plugin.DebugLogger.LogDebug($"{__instance.itemProperties.itemName}'s position is not on the floor.");
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
				Plugin.DebugLogger.LogMessage($"{__instance.itemProperties.itemName} is not on the ship. - calling original method.");
				return true;
			}
		}
	}
}
