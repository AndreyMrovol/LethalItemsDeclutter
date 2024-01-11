// using System.Collections.Generic;
// using BepInEx.Configuration;

// namespace ItemDeclutter;


// public class ItemZoneConfig
// {
//   public static ItemZoneConfig Instance { get; private set; }

//   public static void Init(ConfigFile config)
//   {
//     Instance = new ItemZoneConfig(config);

//     List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

//     foreach (Item item in allItemsList)
//     {
//       config.Bind("Items", item.itemName, "", $"Set zone for {item.itemName}.");
//       Plugin.logger.LogMessage($"Set zone for {item.itemName}.");
//     }
//   }
// }