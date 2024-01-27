using HarmonyLib;
using LethalNetworkAPI;
using System.Collections.Generic;
using UnityEngine;

namespace ItemDeclutter
{

  [HarmonyPatch(typeof(StartOfRound))]
  internal class JoinServerPatch
  {

    // this is my last option, i want to cry
    // When a player join the server, check if lobby has "SupportItemDeclutter" tag
    // then send a message to the host to get host's config
    // and fucking voila

    internal static Dictionary<string, LethalNetworkVariable<Vector3>> ItemZoneSynchronizedConfig = new Dictionary<string, LethalNetworkVariable<Vector3>>();

    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    internal static void PatchLogic()
    {
      bool isHost = StartOfRound.Instance.IsHost;

      Plugin.logger.LogInfo($"JoinServerPatch called - isHost: {isHost}");

      if (isHost) return;

      Plugin.logger.LogInfo($"creating weird shit on client");

      Positions.PositionsDictionary.Clear();

      // // i just give up - how the fuck am i supposed to stop items from spawning **before** getting config from host?
      // // fuck this
      // // this will support only Steam lobbies, but after 10 hours of debbuging i'm done
      // // this might be the most hacky thing i'll do in my lifetime

      if(!GameNetworkManager.Instance.disableSteam && GameNetworkManager.Instance.currentLobby != null){
        // not a LAN lobby, so lobby should have config data attribute
        // it will sync normally after this, but i have to load items in correct zones right now
        // i'm gonna die before getting it right :)

        string serializedConfig = GameNetworkManager.Instance.currentLobby?.GetData("ItemDeclutterConfig");
        ConfigSync.DeserializeConfig(serializedConfig);
      }

      ConfigSync.receiveItemPositionEvent.InvokeServer();
    }

    // [HarmonyPatch("SetShipReadyToLand")]
    // [HarmonyPostfix]
    // internal static void Logic()
    // {
    //   PatchLogic();
    // }

  }
}