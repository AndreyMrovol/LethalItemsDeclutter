using HarmonyLib;

namespace ItemDeclutter
{
	[HarmonyPatch(typeof(StartOfRound))]
	public static class StartOfRoundDisablePatch
	{
		[HarmonyPatch("OnDisable")]
		[HarmonyPostfix]
		public static void PatchLogic()
		{
			Plugin.logger.LogInfo("StartOfRoundDisablePatch called");
			ResetHandler.Reset();
		}
	}
}
