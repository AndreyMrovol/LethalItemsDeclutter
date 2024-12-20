using ItemDeclutter.Managers;

namespace ItemDeclutter
{
	public static class ResetHandler
	{
		public static void Reset()
		{
			DeclutterSync.Instance.SetNewOnHost([]);

			ZoneManager.Reset();
			ItemManager.Reset();
			PositionManager.Reset();
		}
	}
}
