using System;
using System.Security.Cryptography;
using System.Text;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter.Patches
{
	[HarmonyPatch(typeof(GameNetworkManager), "Start")]
	class GameNetworkManagerStartPatch
	{
		[HarmonyPrefix]
		public static void GameMethodPatch(GameNetworkManager __instance)
		{
			DeclutterSync.networkManager = __instance.GetComponent<NetworkManager>();

			var prefab = new GameObject("DeclutterSyncInnit") { hideFlags = HideFlags.HideAndDontSave };

			prefab.AddComponent<NetworkObject>();
			var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("DeclutterSync"));
			prefab.GetComponent<NetworkObject>().GlobalObjectIdHash = BitConverter.ToUInt32(hash, 0);

			prefab.AddComponent<DeclutterSync>();
			prefab.GetComponent<NetworkObject>().DontDestroyWithOwner = true;
			prefab.GetComponent<NetworkObject>().SceneMigrationSynchronization = true;
			prefab.GetComponent<NetworkObject>().DestroyWithScene = false;
			GameObject.DontDestroyOnLoad(prefab);

			DeclutterSync.DeclutterSyncPrefab = prefab;
			DeclutterSync.RegisterNetworkPrefab(prefab);

			DeclutterSync.RegisterPrefabs(__instance.GetComponent<NetworkManager>());

			Plugin.logger.LogDebug("DeclutterSync initialized in GameNetworkManager.Start");
		}
	}
}
