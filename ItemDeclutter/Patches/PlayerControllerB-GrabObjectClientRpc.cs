using GameNetcodeStuff;
using HarmonyLib;
using Unity.Netcode;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(PlayerControllerB))]
	internal class PlayerPickupItem
	{
		[HarmonyPatch("GrabObjectClientRpc")]
		[HarmonyPostfix]
		internal static void GrabObjectPatch(bool grabValidated, ref NetworkObjectReference grabbedObject)
		{
			grabbedObject.TryGet(out NetworkObject networkObject);

			if (networkObject == null)
			{
				return;
			}

			GrabbableObject grabbableObject = networkObject.GetComponent<GrabbableObject>();

			if (grabbableObject == null)
			{
				return;
			}

			string itemName = grabbableObject.itemProperties.itemName;
			grabbableObject.customGrabTooltip = TooltipHelper.GetTooltip(itemName);
		}
	}
}
