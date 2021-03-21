using HarmonyLib;

namespace AntiReflection
{
    [HarmonyPatch(typeof(PLServer), "OnPhotonSerializeView")]
    class Patch
    {
        static void Postfix(PLServer __instance)
        {
            __instance.IsReflection = false;
        }
    }
}
