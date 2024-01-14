using LethalNetworkAPI;
using Newtonsoft.Json;
using UnityEngine;

namespace ItemDeclutter
{

  [JsonObject(MemberSerialization.OptIn)]
  public class PositionData
  {
    [JsonProperty]
    public string ItemName { get; set; }

    [JsonProperty]
    public Vector3 Position { get; set; }
  }

  internal class ConfigSync
  {

    public static LethalServerMessage<string> sendItemPositionData = new("itemPositionData");
    public static LethalClientMessage<string> receiveItemPositionData = new("itemPositionData");

    public static LethalServerEvent sendItemPositionEvent = new("itemPositionEvent");
    public static LethalClientEvent receiveItemPositionEvent = new("itemPositionEvent");


    public static void Start()
    {
      receiveItemPositionData.OnReceived += MessageReceivedFromServer;
      sendItemPositionEvent.OnReceived += ItemPositionRequest;
    }

    private static void MessageReceivedFromServer(string message)
    {
      Plugin.logger.LogInfo($"Message received from server: {message}");

      var JsonDeserialized = JsonConvert.DeserializeObject<PositionData>(message);
      Plugin.logger.LogInfo($"Deserialized: {JsonDeserialized.ItemName} - {JsonDeserialized.Position}");

      Positions.PositionsDictionary[JsonDeserialized.ItemName] = JsonDeserialized.Position;
    }

    private static void ItemPositionRequest(ulong clientId)
    {

      Plugin.logger.LogInfo($"ItemPositionRequest called");

      if (StartOfRound.Instance.IsHost) SendItemsPositionData(clientId);

    }

    public static void SendItemsPositionData(ulong clientId)
    {
      if (StartOfRound.Instance.IsHost)
      {
        Plugin.logger.LogInfo($"Sending item position data to clients");

        foreach (var key in Positions.PositionsDictionary.Keys)
        {
          var entry = Positions.PositionsDictionary[key];

          if (Positions.DroppedItemsYCoordinateDictionary.ContainsKey(key))
          {
            entry.y = Positions.DroppedItemsYCoordinateDictionary[key];
            Plugin.logger.LogInfo($"Updating {key} Y coordinate from dictionary: {entry.y}");
          }

          var JsonSerialized = new PositionData
          {
            ItemName = key,
            Position = entry
          };


          sendItemPositionData.SendClient(JsonConvert.SerializeObject(JsonSerialized, Formatting.None, new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          }), clientId);
          Plugin.logger.LogInfo($"Sending {key} data: {JsonSerialized}");
        }

      }
    }

  }

}