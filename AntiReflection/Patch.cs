using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace AntiReflection
{
    [HarmonyPatch(typeof(PLServer), "OnPhotonSerializeView")]
    class Patch
    {
        static void Postfix(PLServer __instance)
        {
            if (!PhotonNetwork.isMasterClient && __instance.IsReflection)
            {
                __instance.IsReflection = false;
            }
        }
    }
    [HarmonyPatch(typeof(PLServer), "Update")]
    class UpdatePatch
    {
        static void Postfix(PLServer __instance)
        {
            if (PhotonNetwork.isMasterClient && __instance.IsReflection)
            {
                __instance.IsReflection = false;
            }
        }
    }
    [HarmonyPatch(typeof(PLWarpGuardian), "Update")]
    class WGUpdatePatch
    {
        /*static void Postfix(ref bool __InitialReflectionState, ref int __CurrentPhase, ref bool __InitiatedReflection)
        {
            if (PhotonNetwork.isMasterClient && __CurrentPhase == 1 && __InitiatedReflection)
            {
                __InitialReflectionState = true;
                __CurrentPhase = 3;
            }
        }*/
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence2 = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(PLWarpGuardian), "InitiatedReflection")),
                new CodeInstruction(OpCodes.Brtrue_S),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Stfld, AccessTools.Field(typeof(PLWarpGuardian), "InitiatedReflection")),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(PLServer), "Instance")),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Stfld),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Ldstr),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Callvirt),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Brfalse),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldsfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Beq_S),
            };
            List<CodeInstruction> injectedSequence2 = new List<CodeInstruction>()
            {
            };
            return PatchBySequence(instructions, targetSequence2, injectedSequence2, patchMode: PatchMode.REPLACE, CheckMode.NONNULL);
        }
    }
}
