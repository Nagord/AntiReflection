using PulsarModLoader;

namespace AntiReflection
{
    public class Mod : PulsarMod
    {
        public override string Version => "2.0.0";

        public override string Author => "Dragon";

        public override string LongDescription => "Stops reflection and effects.";

        public override string Name => "AntiReflection";

        public override string HarmonyIdentifier()
        {
            return "Dragon.AntiReflection";
        }
    }
}
