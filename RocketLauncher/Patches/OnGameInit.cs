using HarmonyLib;
using RocketLauncher.Launcher;
using RocketLauncher.Utils;
using UnityEngine;

namespace RocketLauncher.Patches
{
    [HarmonyPatch(typeof(GorillaTagger), "Awake")]
    internal class OnGameInit
    {
        public static void Postfix()
        {
            new GameObject("RocketLauncher").AddComponent<PlayerUtils>().gameObject.AddComponent<RocketLauncherWeapon>();
        }
    }
}
