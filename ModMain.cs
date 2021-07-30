using MelonLoader;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

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
		private static MelonPreferences_Entry<float> PreferenceFPS, PreferenceFPSVariance;
		private static MelonPreferences_Entry<int> PreferencePing, PreferencePingVariance;


		public override void OnApplicationStart()
		{
			PreferencesCategory = MelonPreferences.CreateCategory(PreferencesIdentifier, BuildInfo.name);
			PreferenceFPS = PreferencesCategory.CreateEntry("SpoofFPS", -1f, "FPS to spoof to (disable with < 0)");
			PreferencePing = PreferencesCategory.CreateEntry("SpoofPing", -1, "Ping to spoof to (disable with < 0)");
			PreferenceFPSVariance = PreferencesCategory.CreateEntry("SpoofFPSVariance", 0f, "Max random addition to spoofed FPS (disable with <= 0)");
			PreferencePingVariance = PreferencesCategory.CreateEntry("SpoofPingVariance", 0, "Max random addition to spoofed ping (disable with <= 0");

			try
			{
				HarmonyInstance.Patch(
					typeof(Time).GetProperty("smoothDeltaTime").GetGetMethod(),
					prefix: new HarmonyMethod(typeof(NoDetailsForClientersMod).GetMethod("PatchFPS", BindingFlags.NonPublic | BindingFlags.Static))
				);
			}
			catch (System.Exception ex)
			{
				MelonLogger.Msg($"Failed to patch FPS: {ex}");
			}

			try
			{
				HarmonyInstance.Patch(
					typeof(ExitGames.Client.Photon.PhotonPeer).GetProperty("RoundTripTime").GetGetMethod(),
					prefix: new HarmonyMethod(typeof(NoDetailsForClientersMod).GetMethod("PatchPing", BindingFlags.NonPublic | BindingFlags.Static))
				);
			}
			catch (System.Exception ex)
			{
				MelonLogger.Msg($"Failed to patch ping: {ex}");
			}
		}



		private static bool PatchFPS(ref float __result)
		{
			if (PreferenceFPS.Value < 0) return true;
			var val = PreferenceFPS.Value;
			if (PreferenceFPSVariance.Value > 0)
				val += (PreferenceFPSVariance.Value) * (float)(new System.Random().NextDouble());
			__result = 1f / val;
			return false;
		}

		private static bool PatchPing(ref int __result)
		{
			if (PreferencePing.Value < 0) return true;
			var val = PreferencePing.Value;
			if (PreferencePingVariance.Value > 0)
				val += new System.Random().Next(0, PreferencePingVariance.Value);
			__result = val;
			return false;
		}
	}
}
