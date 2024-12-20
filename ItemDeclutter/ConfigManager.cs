using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace ItemDeclutter;

public class ConfigManager
{
	public static ConfigManager Instance { get; private set; }
	public static ConfigFile ConfigFile { get; private set; }

	public static void Init(ConfigFile config)
	{
		Instance = new ConfigManager(config);
		ConfigFile = config;
	}

	public static ConfigEntry<bool> LogCreatedZones { get; private set; }

	public static ConfigEntry<bool> DivideCheapExpensive { get; private set; }
	public static ConfigEntry<int> ExpensiveValue { get; private set; }
	public static ConfigEntry<bool> DivideOneTwoHanded { get; private set; }

	public static ConfigEntry<bool> ShouldItemsAssumeZones { get; private set; }

	public static ConfigEntry<float> ZoningStartX { get; private set; }
	public static ConfigEntry<float> ZoningStartY { get; private set; }
	public static ConfigEntry<float> ZoningStartZ { get; private set; }

	public static ConfigEntry<float> ZoneDistanceX { get; private set; }
	public static ConfigEntry<float> ZoneDistanceY { get; private set; }
	public static ConfigEntry<float> ZoneDistanceZ { get; private set; }

	public static ConfigEntry<int> HowManyZonesX { get; private set; }
	public static ConfigEntry<int> HowManyZonesY { get; private set; }
	public static ConfigEntry<int> HowManyZonesZ { get; private set; }

	private ConfigManager(ConfigFile config)
	{
		DivideCheapExpensive = config.Bind(
			"|Scrap Properties",
			"DivideCheapExpensive",
			true,
			"Divide cheap and expensive items into different zones."
		);
		ExpensiveValue = config.Bind("|Scrap Properties", "ExpensiveValue", 65, "At what point value is considered expensive?");
		DivideOneTwoHanded = config.Bind(
			"|Scrap Properties",
			"DivideOneTwoHanded",
			true,
			"Divide normal and two handed items into different zones."
		);

		ShouldItemsAssumeZones = config.Bind(
			"|Zone Allocation",
			"ShouldItemsAssumeZones",
			false,
			"Should items assume zones if they are not defined in the config?"
		);

		LogCreatedZones = config.Bind("|Logging", "Created Zones", true, "Log created zones in console.");

		ZoningStartX = config.Bind("|Zone Settings: Start Position", "ZoningStartX", -6f, "Start position on X axis (entrance -> console)");
		ZoningStartY = config.Bind("|Zone Settings: Start Position", "ZoningStartY", 0.25f, "Start position on Y axis (floor -> ceiling)");
		ZoningStartZ = config.Bind("|Zone Settings: Start Position", "ZoningStartZ", -5f, "Start position on Z axis (left -> right)");

		ZoneDistanceX = config.Bind(
			"|Zone Settings: Distance between zones",
			"ZoneDistanceX",
			0.65f,
			"Distance between zones on X axis (entrance -> console)"
		);
		ZoneDistanceY = config.Bind(
			"|Zone Settings: Distance between zones",
			"ZoneDistanceY",
			1.25f,
			"Distance between zones on Y axis (floor -> ceiling)"
		);
		ZoneDistanceZ = config.Bind(
			"|Zone Settings: Distance between zones",
			"ZoneDistanceZ",
			1f,
			"Distance between zones on Z axis (left -> right)"
		);

		HowManyZonesX = config.Bind(
			"|Zone Settings: How many zones to create",
			"HowManyZonesX",
			20,
			"How many zones on X axis (entrance -> console)"
		);
		HowManyZonesY = config.Bind(
			"|Zone Settings: How many zones to create",
			"HowManyZonesY",
			5,
			"How many zones on Y axis (floor -> ceiling)"
		);
		HowManyZonesZ = config.Bind("|Zone Settings: How many zones to create", "HowManyZonesZ", 4, "How many zones on Z axis (left -> right)");
	}
}
