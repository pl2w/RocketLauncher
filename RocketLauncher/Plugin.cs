using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RocketLauncher
{
    [BepInPlugin("pl2w.rocketlauncher", "RocketLauncher", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        Plugin() => new Harmony("pl2w.rocketlauncher").PatchAll(Assembly.GetExecutingAssembly());
    }
}for ex
