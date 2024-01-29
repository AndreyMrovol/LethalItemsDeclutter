using System.Collections.Generic;
using System.Linq;
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

    public static LethalServerMessage<string> sendItemConfig = new("configData");
    public static LethalClientMessage<string> receiveItemConfig = new("configData");

    public static LethalServerMessage<string> sendItemPosition = new("itemPositionData");
    public static LethalClientMessage<string> receiveItemPosition = new("itemPositionData");

    public static LethalServerEvent sendItemPositionEvent = new("itemPositionEvent");
    public static LethalClientEvent receiveItemPositionEvent = new("itemPositionEvent");

    public static LethalNetworkVariable<string> synchronizedConfig = new("synchronizedConfig");


    public static void Start()
    {
      sendItemPositionEvent.OnReceived += ItemPositionRequest;
      synchronizedConfig.OnValueChanged += MessageReceivedFromServer;
      receiveItemPosition.OnReceived += ItemPositionReceived;
    }

    private static void MessageReceivedFromServer(string message)
    {

      if(StartOfRound.Instance.IsHost) return;

      Plugin.logger.LogInfo($"Config received from server: {message}");

      var JsonDeserialized = JsonConvert.DeserializeObject<List<PositionData>>(message);

      foreach (var item in JsonDeserialized)
      {
        Plugin.logger.LogInfo($"Deserialized: {item.ItemName} - {item.Position}");
        Positions.PositionsDictionary[item.ItemName] = item.Position;
      }
    }

    private static void ItemPositionReceived(string message){

      if(StartOfRound.Instance.IsHost) return;
      Plugin.logger.LogInfo($"Item settings received from server: {message}");

      var JsonDeserialized = JsonConvert.DeserializeObject<PositionData>(message);

      Positions.PositionsDictionary[JsonDeserialized.ItemName] = JsonDeserialized.Position;
    }

    private static void ItemPositionRequest(ulong clientId)
    {

      Plugin.logger.LogInfo($"ItemPositionRequest called");

      if (StartOfRound.Instance.IsHost) SendItemsPositionData(clientId);

    }

    public static void SendItemsPositionData(ulong clientId)
    {
      List<PositionData> PositionList = GetItemsPositionData();

      sendItemConfig.SendClient(JsonConvert.SerializeObject(PositionList, Formatting.None, new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          }), clientId);
          Plugin.logger.LogInfo($"Sending data: {PositionList}");
    }

    public static void SetNetworkedConfig(){
      List<PositionData> PositionList = GetItemsPositionData();

      synchronizedConfig.Value = JsonConvert.SerializeObject(PositionList, Formatting.None, new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          });
          Plugin.logger.LogInfo($"Setting networked config: {PositionList}");

      GameNetworkManager.Instance.currentLobby?.SetData("ItemDeclutterConfig", synchronizedConfig.Value);
    }

    public static List<PositionData> GetItemsPositionData(){
      List<PositionData> PositionList = new List<PositionData>();

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

          Plugin.logger.LogInfo($"Serialized: {JsonSerialized.ItemName} - {JsonSerialized.Position}");
          PositionList.Add(JsonSerialized);
      }

      return PositionList;
    }

    public static void DeserializeConfig(string serialized){
      if(serialized == "" || serialized == null) return;

      var JsonDeserialized = JsonConvert.DeserializeObject<List<PositionData>>(serialized);

      foreach (var item in JsonDeserialized)
      {
        Plugin.logger.LogInfo($"Deserialized: {item.ItemName} - {item.Position}");
        Positions.PositionsDictionary[item.ItemName] = item.Position;
      }
    }

    public static string GetSerializedConfig(){
      return JsonConvert.SerializeObject(GetItemsPositionData(), Formatting.None, new JsonSerializerSettings
          {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          });
    }

    public static void UpdateItemPositionData(GrabbableObject item)
    {

      if (StartOfRound.Instance.IsHost)
      {
        Plugin.logger.LogInfo($"Updating item position data");

        var JsonSerialized = new PositionData
        {
          ItemName = item.itemProperties.itemName,
          Position = Positions.PositionsDictionary[item.itemProperties.itemName]
        };

        sendItemPosition.SendAllClients(JsonConvert.SerializeObject(JsonSerialized, Formatting.None, new JsonSerializerSettings
        {
          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
        Plugin.logger.LogInfo($"Sending {item.itemProperties.itemName} data: {JsonSerialized}");
      }

    }

  }

}