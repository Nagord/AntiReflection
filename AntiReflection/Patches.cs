using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace AntiReflection
{
    [HarmonyPatch(typeof(PLPostProcessVignette), "OnRenderImage")]
    class VignettePatch
    {
        static float PatchMethod()
        {
            return (Global.CanRenderReflection.Value && PLServer.Instance != null && PLServer.Instance.IsReflection) ? 1f : 0f;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(PLServer), "Instance")),
                new CodeInstruction(OpCodes.Ldnull),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Brfalse_S),
                new CodeInstruction(OpCodes.Ldsfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Brtrue_S),
                new CodeInstruction(OpCodes.Ldc_R4),
                new CodeInstruction(OpCodes.Br_S),
                new CodeInstruction(OpCodes.Ldc_R4),
            };
            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(VignettePatch), "PatchMethod"))
            };
            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE, CheckMode.NONNULL);
        }
    }


    [HarmonyPatch(typeof(PLPostProcessReflection), "OnRenderImage")]
    class ReflectionPatch
    {
        static bool PatchMethod(PLPostProcessReflection instance)
        {
            float SmoothReflectionPercent = instance.SmoothReflectionPercent;
            bool Reflection = Global.CanRenderReflection && PLServer.Instance != null && PLServer.Instance.IsReflection;
            if (!Global.CanRenderReflectionTransition && (SmoothReflectionPercent != 0f || SmoothReflectionPercent != 1))
            {
                instance.SmoothReflectionPercent = Reflection ? 1f : 0f;
            }
            return Reflection;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(PLServer), "Instance")),
                new CodeInstruction(OpCodes.Ldnull),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Brfalse_S),
                new CodeInstruction(OpCodes.Ldsfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Br_S),
                new CodeInstruction(OpCodes.Ldc_I4_0),
            };
            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ReflectionPatch), "PatchMethod"))
            };
            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE, CheckMode.NONNULL);
        }
    }
}
