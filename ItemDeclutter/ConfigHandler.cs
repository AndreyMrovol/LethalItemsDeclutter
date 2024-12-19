using System.Collections.Generic;
using System.Numerics;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UIElements.Collections;

namespace ItemDeclutter
{
	public abstract class ConfigHandler<T, CT> : MrovLib.ConfigHandler<T, CT>
	{
		public ConfigHandler(CT value)
		{
			DefaultValue = value;
		}
	}

	public class ZoneConfigHandler : ConfigHandler<UnityEngine.Vector3, string>
	{
		public ZoneConfigHandler(string value)
			: base(value) { }

		public override UnityEngine.Vector3 Value
		{
			get { return Positions.Zones.GetValueOrDefault(DefaultValue); }
		}
	}
}
