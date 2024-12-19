using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using UnityEngine;

namespace ItemDeclutter
{
	public class Positions
	{
		// dictionary of positions and item names

		public static Vector3 vectorConstrctor(double x, double y, double z)
		{
			return new Vector3((float)x, (float)y, (float)z);
		}

		public static Dictionary<string, Vector3> Zones = [];

		public static Dictionary<string, Boolean> ZoneAllocated = [];

		public static Dictionary<string, float> DroppedItemsYCoordinateDictionary = [];

		public static Dictionary<string, Vector3> PositionsDictionary = [];
	}
}
