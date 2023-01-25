using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace AntiReflection
{
    internal class Commands : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "antireflection", "af" };
        }

        public override string Description()
        {
            return "Toggles reflection and reflection transition setting. Use /af reflection and /af transition";
        }

        public override void Execute(string arguments)
        {
            if(arguments.ToLower().StartsWith("t"))
            {
                Global.CanRenderReflectionTransition.Value = !Global.CanRenderReflectionTransition.Value;
            }
            else if (arguments.ToLower().StartsWith("r"))
            {
                Global.CanRenderReflection.Value = !Global.CanRenderReflection.Value;
            }
            Messaging.Notification($"Reflection: {(Global.CanRenderReflection.Value ? "Enabled" : "Disabled")}\nTransition: {(Global.CanRenderReflectionTransition.Value ? "Enabled" : "Disabled")}");
        }
    }
}
