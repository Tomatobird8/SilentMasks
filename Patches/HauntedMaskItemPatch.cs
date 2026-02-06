using HarmonyLib;

namespace SilentMasks.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]
    public class HauntedMaskItemPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void StartPatch(GrabbableObject __instance)
        {
            if (__instance is HauntedMaskItem)
            {
                RandomPeriodicAudioPlayer rpap = __instance.transform.GetComponent<RandomPeriodicAudioPlayer>();
                if (rpap != null)
                {
                    rpap.enabled = false;
                }
            }
        }
    }
}
