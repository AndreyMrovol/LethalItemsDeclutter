using System.Numerics;

namespace ItemDeclutter.Definitions
{
	public interface IItemConfig
	{
		public string ObjectName { get; }
		public UnityEngine.Vector3 Coordinates { get; }
	}

	public class ItemConfig : IItemConfig
	{
		public Item Item { get; set; }
		public string ObjectName
		{
			get { return Item.itemName; }
		}

		public ZoneConfigHandler ZoneConfig;
		public string Zone
		{
			get { return ZoneConfig.RawValue; }
		}

		public UnityEngine.Vector3 Coordinates
		{
			get { return ZoneConfig.Value; }
		}

		public float XCoordinate => Coordinates.x;
		public float YCoordinate => Coordinates.y;
		public float ZCoordinate => Coordinates.z;

		public override string ToString()
		{
			return $"{ObjectName} ({(Item.isScrap ? "Scrap" : "Item")})";
		}
	}
}
