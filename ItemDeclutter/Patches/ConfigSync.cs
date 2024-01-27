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

    public static LethalServerMessage<string> sendItemPositionData = new("itemPositionData");
    public static LethalClientMessage<string> receiveItemPositionData = new("itemPositionData");

    public static LethalServerMessage<string> sendItemLocalPosition = new("itemLocalPosition");
    public static LethalClientMessage<string> receiveItemLocalPosition = new("itemLocalPosition");

    public static LethalServerEvent sendItemPositionEvent = new("itemPositionEvent");
    public static LethalClientEvent receiveItemPositionEvent = new("itemPositionEvent");

    public static LethalNetworkVariable<string> synchronizedConfig = new("synchronizedConfig");


    public static void Start()
    {
      // receiveItemPositionData.OnReceived += MessageReceivedFromServer;
      sendItemPositionEvent.OnReceived += ItemPositionRequest;

      synchronizedConfig.OnValueChanged += MessageReceivedFromServer;
    }

    private static void MessageReceivedFromServer(string message)
    {

      if(StartOfRound.Instance.IsHost) return;

      Plugin.logger.LogInfo($"Message received from server: {message}");

      var JsonDeserialized = JsonConvert.DeserializeObject<List<PositionData>>(message);

      foreach (var item in JsonDeserialized)
      {
        Plugin.logger.LogInfo($"Deserialized: {item.ItemName} - {item.Position}");
        Positions.PositionsDictionary[item.ItemName] = item.Position;
      }

      ConfigManager.IsInitialized = true;
      // ShipResolveItems.Patch();
    }

    private static void ItemPositionRequest(ulong clientId)
    {

      Plugin.logger.LogInfo($"ItemPositionRequest called");

      if (StartOfRound.Instance.IsHost) SendItemsPositionData(clientId);

    }

    public static void SendItemsPositionData(ulong clientId)
    {
      List<PositionData> PositionList = GetItemsPositionData();

      sendItemPositionData.SendClient(JsonConvert.SerializeObject(PositionList, Formatting.None, new JsonSerializerSettings
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

        sendItemPositionData.SendAllClients(JsonConvert.SerializeObject(JsonSerialized, Formatting.None, new JsonSerializerSettings
        {
          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
        Plugin.logger.LogInfo($"Sending {item.itemProperties.itemName} data: {JsonSerialized}");
      }

    }

  }

}