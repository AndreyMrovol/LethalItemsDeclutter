using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(StartOfRound))]
	public class StartOfRoundAwakePatch
	{
		[HarmonyPatch("Awake")]
		[HarmonyPrefix]
		[HarmonyPriority(Priority.First)]
		internal static void StartOfRoundAwakePrefix(RoundManager __instance)
		{
			Plugin.logger.LogInfo("StartOfRoundAwakePrefix Patch");

			// Plugin.logger.LogDebug(GameNetworkManager.Instance);
			// Plugin.logger.LogDebug(GameNetworkManager.Instance.GetComponent<NetworkManager>());
			// Plugin.logger.LogDebug(DeclutterSync.DeclutterSyncPrefab);
			// Plugin.logger.LogDebug(DeclutterSync.DeclutterSyncPrefab.GetComponent<DeclutterSync>());
			// Plugin.logger.LogDebug(DeclutterSync.DeclutterSyncPrefab.GetComponent<NetworkObject>());

			Plugin.logger.LogWarning(GameNetworkManager.Instance.GetComponent<NetworkManager>().IsHost);

			if (GameNetworkManager.Instance.GetComponent<NetworkManager>().IsHost)
			{
				Plugin.logger.LogDebug("Host detected, spawning DeclutterSync");
				DeclutterSync DeclutterSyncPrefab = GameObject.Instantiate(DeclutterSync.DeclutterSyncPrefab).GetComponent<DeclutterSync>();
				DeclutterSyncPrefab.GetComponent<NetworkObject>().Spawn(destroyWithScene: false);
			}
		}
	}
}
