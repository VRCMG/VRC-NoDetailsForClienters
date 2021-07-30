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
		private static float VarianceFPS = 0f;
		private static MelonPreferences_Entry<int> PreferencePing, PreferencePingVariance, PreferencVarianceMin, PreferencVarianceMax;
		private static int VariancePing = 0;
		private System.DateTime _next_variance_update_after = System.DateTime.Now;

		public override void OnApplicationStart()
		{
			PreferencesCategory = MelonPreferences.CreateCategory(PreferencesIdentifier, BuildInfo.name);
			PreferenceFPS = PreferencesCategory.CreateEntry("SpoofFPS", -1f, "FPS to spoof to (disable with < 0)");
			PreferencePing = PreferencesCategory.CreateEntry("SpoofPing", -1, "Ping to spoof to (disable with < 0)");
			PreferenceFPSVariance = PreferencesCategory.CreateEntry("SpoofFPSVariance", 0f, "Max random addition to spoofed FPS (disable with <= 0)");
			PreferencePingVariance = PreferencesCategory.CreateEntry("SpoofPingVariance", 0, "Max random addition to spoofed ping (disable with <= 0");
			PreferencVarianceMin = PreferencesCategory.CreateEntry("VarianceMinInterval", 800, "Min interval variance");
			PreferencVarianceMax = PreferencesCategory.CreateEntry("VarianceMaxInterval", 3000, "Max interval variance");

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

		public override void OnFixedUpdate()
		{
			if (_next_variance_update_after < System.DateTime.Now)
			{
				var rng = new System.Random();
				_next_variance_update_after =
					System.DateTime.Now.AddMilliseconds(rng.Next(PreferencVarianceMin.Value, PreferencVarianceMax.Value));

				if (PreferenceFPSVariance.Value <= 0) VarianceFPS = 0f;
				else VarianceFPS = (PreferenceFPSVariance.Value) * (float)rng.NextDouble();

				if (PreferencePingVariance.Value <= 0) VariancePing = 0;
				else VariancePing = rng.Next(0, PreferencePingVariance.Value);
			}
		}



		private static bool PatchFPS(ref float __result)
		{
			if (PreferenceFPS.Value < 0) return true;
			var val = PreferenceFPS.Value;

			__result = 1f / (val + VarianceFPS);
			return false;
		}

		private static bool PatchPing(ref int __result)
		{
			if (PreferencePing.Value < 0) return true;
			__result = PreferencePing.Value + VariancePing;
			return false;
		}
	}
}
