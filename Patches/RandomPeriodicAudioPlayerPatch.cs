using HarmonyLib;

namespace SilentMasks.Patches
{
    [HarmonyPatch(typeof(RandomPeriodicAudioPlayer))]
    public class RandomPeriodicAudioPlayerPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static void PatchIntervalCheck(RandomPeriodicAudioPlayer __instance)
        {
            __instance.enabled = false;
        }
    }
}
