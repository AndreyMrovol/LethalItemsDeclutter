using System.Collections.Generic;
using HarmonyLib;
using ItemDeclutter.Managers;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	internal class JoinServerPatch
	{
		// internal static Dictionary<string, LethalNetworkVariable<Vector3>> ItemZoneSynchronizedConfig = [];

		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		internal static void PatchLogic()
		{
			bool isHost = StartOfRound.Instance.IsHost;

			Plugin.logger.LogInfo($"JoinServerPatch called - isHost: {isHost}");

			if (isHost)
			{
				return;
			}

			Plugin.logger.LogInfo($"creating weird shit on client");

			PositionManager.ItemCoordinates.Clear();
		}
	}
}
