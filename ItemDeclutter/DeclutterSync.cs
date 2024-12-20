using System.Collections.Generic;
using System.Linq;
using ItemDeclutter.Definitions;
using Newtonsoft.Json;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter
{
	internal class DeclutterSync : NetworkBehaviour
	{
		public NetworkVariable<ItemZoneDataWrapper> ItemZones = new(new ItemZoneDataWrapper { ItemZonesData = [] });
		public ItemZoneData[] ItemZonesData
		{
			get => ItemZones.Value.ItemZonesData;
			set => ItemZones.Value = new ItemZoneDataWrapper { ItemZonesData = value };
		}

		public static GameObject DeclutterSyncPrefab;
		public static NetworkManager networkManager;
		public static bool networkHasStarted = false;
		internal static DeclutterSync _instance;
		private static List<GameObject> queuedNetworkPrefabs = [];

		public static DeclutterSync Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UnityEngine.Object.FindObjectOfType<DeclutterSync>();
					if (_instance == null)
					{
						Plugin.logger.LogError("DeclutterSync instance is null");
					}
				}
				return _instance;
			}
			private set => _instance = value;
		}

		public override void OnNetworkSpawn()
		{
			base.OnNetworkSpawn();
			InitializeInstance();
			ItemZones.OnValueChanged += Networking.ItemZones.ItemZonesReceived;

			Plugin.logger.LogDebug("DeclutterSync OnNetworkSpawn");
		}

		public void SetNewOnHost(Dictionary<string, Vector3> ItemLocations)
		{
			Plugin.logger.LogDebug("Setting new item zones on host");

			if (!StartOfRound.Instance.IsHost)
			{
				Plugin.logger.LogDebug("Cannot set item zones, not a host!");
				return;
			}

			ItemZoneData[] itemZones = ItemLocations
				.Select(x => new ItemZoneData
				{
					ObjectName = x.Key,
					XCoordinate = x.Value.x,
					YCoordinate = x.Value.y,
					ZCoordinate = x.Value.z
				})
				.ToArray();

			Plugin.logger.LogDebug(
				JsonConvert.SerializeObject(
					itemZones,
					Formatting.None,
					new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
				)
			);

			ItemZones.Set(new ItemZoneDataWrapper { ItemZonesData = itemZones });
		}

		#region Prefab registration

		public static void RegisterNetworkPrefab(GameObject prefab)
		{
			if (!networkHasStarted)
			{
				Plugin.logger.LogDebug("Registering NetworkPrefab: " + prefab);
				queuedNetworkPrefabs.Add(prefab);
			}
			else
			{
				Plugin.logger.LogWarning("Attempted To Register NetworkPrefab: " + prefab + " After GameNetworkManager Has Started!");
			}
		}

		internal static void RegisterPrefabs(NetworkManager networkManager)
		{
			Plugin.logger.LogDebug("Registering NetworkPrefabs in NetworkManager");
			var addedNetworkPrefabs = GetExistingPrefabs(networkManager);
			RegisterQueuedPrefabs(networkManager, addedNetworkPrefabs);
			networkHasStarted = true;
		}

		private void InitializeInstance()
		{
			gameObject.name = "DeclutterSync";
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Plugin.logger.LogDebug($"Declutter data: {ItemZones.Value}");
		}

		private static List<GameObject> GetExistingPrefabs(NetworkManager networkManager)
		{
			var addedNetworkPrefabs = new List<GameObject>();
			foreach (NetworkPrefab networkPrefab in networkManager.NetworkConfig.Prefabs.m_Prefabs)
			{
				addedNetworkPrefabs.Add(networkPrefab.Prefab);
			}
			return addedNetworkPrefabs;
		}

		private static void RegisterQueuedPrefabs(NetworkManager networkManager, List<GameObject> addedNetworkPrefabs)
		{
			int debugCounter = 0;
			foreach (GameObject queuedNetworkPrefab in queuedNetworkPrefabs)
			{
				Plugin.logger.LogDebug("Trying To Register Prefab: " + queuedNetworkPrefab);
				if (!addedNetworkPrefabs.Contains(queuedNetworkPrefab))
				{
					networkManager.AddNetworkPrefab(queuedNetworkPrefab);
					addedNetworkPrefabs.Add(queuedNetworkPrefab);
				}
				else
					debugCounter++;
			}
			Plugin.logger.LogDebug("Skipped Registering " + debugCounter + " NetworkObjects As They Were Already Registered.");
		}

		#endregion
	}
}
