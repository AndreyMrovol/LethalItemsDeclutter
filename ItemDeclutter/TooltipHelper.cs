using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using ItemDeclutter.Managers;
using Unity.Netcode;
using UnityEngine;

namespace ItemDeclutter
{
	class TooltipHelper
	{
		public static string GetTooltip(string itemName)
		{
			GameObject ship = GameObject.Find("/Environment/HangarShip");

			var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>()
				.Where(obj => obj.itemProperties.itemName == itemName)
				.Where(obj => !obj.heldByPlayerOnServer)
				.ToList();
			var ListClone = ItemsOnShip.ToList();

			return GetTooltip(ListClone, itemName);
		}

		public static string GetTooltip(List<GrabbableObject> ItemsOnShip, string itemName)
		{
			var count = ItemsOnShip.Count();
			Plugin.logger.LogInfo($"Found {count} {itemName} on ship");

			string tooltip = $"{itemName} (x{count}) \n Grab : [E]";

			return tooltip;
		}

		public static string UpdateAllTooltips(string itemName)
		{
			GameObject ship = GameObject.Find("/Environment/HangarShip");

			var ItemsOnShip = ship.GetComponentsInChildren<GrabbableObject>()
				.Where(obj => obj.itemProperties.itemName == itemName)
				.Where(obj => !obj.heldByPlayerOnServer)
				.ToList();
			var ListClone = ItemsOnShip.ToList();

			if (ListClone.Count() == 0)
			{
				return null;
			}

			if (!PositionManager.IsItemDefined(itemName))
			{
				return null;
			}

			ListClone.Do(item =>
			{
				if (item.playerHeldBy != null)
				{
					Plugin.logger.LogDebug($"Found a player holding {item.itemProperties.itemName}");
					ItemsOnShip.Remove(item);
				}
			});

			string tooltip = GetTooltip(ItemsOnShip, itemName);

			foreach (var item in ItemsOnShip)
			{
				item.customGrabTooltip = tooltip;
			}

			return tooltip;
		}

		public static string UpdateAllTooltips(GrabbableObject grabbable)
		{
			return UpdateAllTooltips(grabbable.itemProperties.itemName);
		}
	}
}
