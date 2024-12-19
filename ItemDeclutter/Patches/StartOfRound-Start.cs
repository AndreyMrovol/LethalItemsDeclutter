using System.Collections.Generic;
using HarmonyLib;
using LethalNetworkAPI;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	internal class JoinServerPatch
	{
		internal static Dictionary<string, LethalNetworkVariable<Vector3>> ItemZoneSynchronizedConfig = [];

		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		internal static void PatchLogic()
		{
			bool isHost = StartOfRound.Instance.IsHost;

			Plugin.logger.LogInfo($"JoinServerPatch called - isHost: {isHost}");

			if (isHost)
				return;

			Plugin.logger.LogInfo($"creating weird shit on client");

			Positions.PositionsDictionary.Clear();

			// // i just give up - how the fuck am i supposed to stop items from spawning **before** getting config from host?
			// // fuck this
			// // this will support only Steam lobbies, but after 10 hours of debbuging i'm done
			// // this might be the most hacky thing i'll do in my lifetime

			if (!GameNetworkManager.Instance.disableSteam && GameNetworkManager.Instance.currentLobby != null)
			{
				// not a LAN lobby, so lobby should have config data attribute
				// it will sync normally after this, but i have to load items in correct zones right now
				// i'm gonna die before getting it right :)

				string serializedConfig = GameNetworkManager.Instance.currentLobby?.GetData("ItemDeclutterConfig");
				ConfigSync.DeserializeConfig(serializedConfig);
			}

			ConfigSync.receiveItemPositionEvent.InvokeServer();
		}
	}
}
