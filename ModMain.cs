using MelonLoader;
using UnityEngine;
using HarmonyLib;

[assembly: MelonInfo(typeof(NoDetailsForClienters.NoDetailsForClientersMod), NoDetailsForClienters.BuildInfo.name, NoDetailsForClienters.BuildInfo.version, NoDetailsForClienters.BuildInfo.authors, NoDetailsForClienters.BuildInfo.downloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
namespace NoDetailsForClienters
{

	public static class BuildInfo
	{
		public const string authors = "ljoonal";

		public const string company = null;

		public const string downloadLink = "https://github.com/ljoonal/VRC-NoDetailsForClienters";

		public const string name = "No Details For Clienters";

		public const string version = "0.0.1";
	}

	public class NoDetailsForClientersMod : MelonMod
	{
		private const string PreferencesIdentifier = "NoDetailsForClienters";
		private static MelonPreferences_Category PreferencesCategory;
		private static MelonPreferences_Entry<int> PreferenceFPS, PreferencePing;

		public override void OnApplicationStart()
		{
			PreferencesCategory = MelonPreferences.CreateCategory(PreferencesIdentifier, BuildInfo.name);
			PreferenceFPS = PreferencesCategory.CreateEntry("SpoofFPS", -1, "FPS to spoof to (disable with negative value)");
			PreferencePing = PreferencesCategory.CreateEntry("SpoofPing", -1, "Ping to spoof to (disable with negative value)");
		}

		[HarmonyPatch(typeof(Time), "smoothDeltaTime", MethodType.Getter)]
		[HarmonyPrefix]
		static bool PatchFPS(ref float __result)
		{
			if (PreferenceFPS.Value < 0) return true;
			__result = PreferenceFPS.Value;
			return false;
		}

		[HarmonyPatch(typeof(ExitGames.Client.Photon.PhotonPeer), "RoundTripTime", MethodType.Getter)]
		[HarmonyPrefix]
		static bool PatchPing(ref int __result)
		{
			if (PreferencePing.Value < 0) return true;
			__result = PreferencePing.Value;
			return false;
		}
	}
}