using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SilentMasks.Patches;

namespace SilentMasks
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class SilentMasks : BaseUnityPlugin
    {
        public static SilentMasks Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        public static bool onlyTargetMaskItems;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            onlyTargetMaskItems = Config.Bind("General", "Only target mask items", true, "Should only mask items be targeted by this fix? If true, patches GrabbableObject.Start to check whether the current class is of type HauntedMaskItem and if so, disables RandomPeriodicAudioPlayer from the object. If false, RandomPeriodicAudioPlayer is patched directly which is faster but some mods may rely on this script being available.").Value;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            Logger.LogDebug("Patching...");

            if (onlyTargetMaskItems)
            {
                Harmony.PatchAll(typeof(HauntedMaskItemPatch));
            }
            else
            {
                Harmony.PatchAll(typeof(RandomPeriodicAudioPlayerPatch));
            }

            Logger.LogDebug("Finished patching!");
        }
    }
}
