using System.Collections.Generic;
using ItemDeclutter.Definitions;

namespace ItemDeclutter.Managers
{
	public static class ItemManager
	{
		public static List<ItemConfig> Grabbables = [];
		public static List<ItemConfig> Items => Grabbables.FindAll(x => x.Item.isScrap == false);
		public static List<ItemConfig> Scraps => Grabbables.FindAll(x => x.Item.isScrap == true);

		internal static void PopulateItemScrapConfig()
		{
			List<Item> allItemsList = StartOfRound.Instance.allItemsList.itemsList;

			foreach (Item item in allItemsList)
			{
				string ItemName = item.itemName;

				ItemConfig itemConfig =
					new()
					{
						Item = item,
						ZoneConfig = new(Defaults.DefaultZones.ContainsKey(ItemName) ? Defaults.DefaultZones[ItemName] : "", ItemName)
					};

				Grabbables.Add(itemConfig);

				if (Defaults.DefaultZones.ContainsKey(ItemName))
				{
					Plugin.logger.LogMessage($"{ItemName} has a default zone : {Defaults.DefaultZones[ItemName]}");
				}

				Plugin.logger.LogMessage($"Added config entry for {item.itemName}.");
			}
		}

		internal static void Reset()
		{
			Grabbables.Clear();
		}
	}
}
