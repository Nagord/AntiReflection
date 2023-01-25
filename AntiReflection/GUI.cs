using PulsarModLoader.CustomGUI;
using static UnityEngine.GUILayout;

namespace AntiReflection
{
    internal class GUI : ModSettingsMenu
    {
        public override void Draw()
        {
            if(Button($"Can Render Reflection: {Global.CanRenderReflection.Value.ToString()}"))
            {
                Global.CanRenderReflection.Value = !Global.CanRenderReflection.Value;
            }

            if (Button($"Can Render Reflection Transition: {Global.CanRenderReflectionTransition.Value.ToString()}"))
            {
                Global.CanRenderReflectionTransition.Value = !Global.CanRenderReflectionTransition.Value;
            }
        }

        public override string Name()
        {
            return "Anti-Reflection";
        }
    }
}
