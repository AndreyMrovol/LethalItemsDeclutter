using Unity.Collections;
using Unity.Netcode;

namespace ItemDeclutter.Definitions
{
	public struct ItemZoneData : INetworkSerializable
	{
		public string ObjectName;
		public UnityEngine.Vector3 Coordinates;

		public void NetworkSerialize<T>(BufferSerializer<T> serializer)
			where T : IReaderWriter
		{
			serializer.SerializeValue(ref ObjectName);
			serializer.SerializeValue(ref Coordinates);
		}

		public override readonly string ToString()
		{
			return $"ObjectName: {ObjectName}, Coordinates: {Coordinates}";
		}
	}

	public struct ItemZoneDataWrapper : INetworkSerializable
	{
		public ItemZoneData[] ItemZonesData;

		public void NetworkSerialize<T>(BufferSerializer<T> serializer)
			where T : IReaderWriter
		{
			int length = 0;
			if (!serializer.IsReader)
			{
				length = ItemZonesData.Length;
			}

			serializer.SerializeValue(ref length);

			if (serializer.IsReader)
			{
				ItemZonesData = new ItemZoneData[length];
			}

			for (int i = 0; i < length; i++)
			{
				serializer.SerializeValue(ref ItemZonesData[i]);
			}
		}
	}
}
