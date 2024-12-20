using ItemDeclutter.Definitions;
using ItemDeclutter.Managers;

namespace ItemDeclutter.Networking
{
	public class ItemZones
	{
		internal static void ItemZonesReceived(ItemZoneDataWrapper previousValue, ItemZoneDataWrapper newValue)
		{
			if (StartOfRound.Instance.IsHost)
			{
				return;
			}

			PositionManager.ItemCoordinates.Clear();
			PositionManager.SetDictionaryFromSyncedData(newValue.ItemZonesData);
		}
	}
}
