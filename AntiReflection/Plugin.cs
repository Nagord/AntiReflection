using PulsarPluginLoader;

namespace AntiReflection
{
    public class Plugin : PulsarPlugin
    {
        public override string Version => "0.0.1";

        public override string Author => "Dragon";

        public override string LongDescription => "Text";

        public override string Name => "AntiReflection";

        public override string HarmonyIdentifier()
        {
            return "Dragon.AntiReflection";
        }
    }
}
