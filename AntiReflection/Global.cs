using PulsarModLoader;

namespace AntiReflection
{
    internal class Global
    {
        public static SaveValue<bool> CanRenderReflection = new SaveValue<bool>("CanRenderReflection", false);
        public static SaveValue<bool> CanRenderReflectionTransition = new SaveValue<bool>("CanRenderReflectionTransition", false);
    }
}
