using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using BepInEx.Configuration;
using ItemDeclutter.Managers;
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
		public ZoneConfigHandler(string defaultValue, string itemName)
			: base(defaultValue)
		{
			string safeName = Regex.Replace(itemName, @"[=\n\t\\""\'\[\]]", "_");
			this.ConfigEntry = ConfigManager.ConfigFile.Bind(safeName, defaultValue, defaultValue, $"Set zone for {itemName}.");
		}

		public string RawValue => ConfigEntry.Value;

		public override UnityEngine.Vector3 Value
		{
			get { return ZoneManager.Zones.GetValueOrDefault(DefaultValue); }
		}
	}
}
