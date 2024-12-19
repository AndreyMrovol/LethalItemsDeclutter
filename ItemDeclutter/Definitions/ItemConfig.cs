using System.Numerics;

namespace ItemDeclutter.Definitions
{
	public class ItemConfig
	{
		public GrabbableObject GrabbableObject { get; set; }

		private static ZoneConfigHandler ZoneConfig;

		public string Zone
		{
			get { return ZoneConfig.DefaultValue; }
		}

		public UnityEngine.Vector3 Coordinates
		{
			get { return ZoneConfig.Value; }
		}

		public float XCoordinate => Coordinates.x;
		public float YCoordinate => Coordinates.y;
		public float ZCoordinate => Coordinates.z;
	}
}
