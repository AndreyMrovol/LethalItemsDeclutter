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

    [HarmonyPatch("PlayerLoadedClientRpc")]
    [HarmonyPostfix]
    internal static void PatchLogic()
    {
      bool isHost = StartOfRound.Instance.IsHost;

      Plugin.logger.LogInfo($"JoinServerPatch called - isHost: {isHost}");

      if (isHost) return;

      Plugin.logger.LogInfo($"creating weird shit on client");

      Positions.PositionsDictionary.Clear();

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