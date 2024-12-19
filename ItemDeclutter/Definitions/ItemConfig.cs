using System.Numerics;

namespace ItemDeclutter.Definitions
{
	public interface IItemConfig
	{
		public string ObjectName { get; }
		public UnityEngine.Vector3 Coordinates { get; }
	}

	public class ItemConfigBase : IItemConfig
	{
		public virtual string ObjectName { get; set; }
		public virtual UnityEngine.Vector3 Coordinates { get; set; }
	}

	public class ItemConfig : ItemConfigBase
	{
		public GrabbableObject GrabbableObject { get; set; }
		public override string ObjectName
		{
			get { return GrabbableObject.itemProperties.itemName; }
		}

		private static ZoneConfigHandler ZoneConfig;

		public string Zone
		{
			get { return ZoneConfig.DefaultValue; }
		}

		public override UnityEngine.Vector3 Coordinates
		{
			get { return ZoneConfig.Value; }
		}

		public float XCoordinate => Coordinates.x;
		public float YCoordinate => Coordinates.y;
		public float ZCoordinate => Coordinates.z;
	}
}
