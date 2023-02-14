using ChatbotConstructorTelegram.Infrastructure.Manager;

namespace ChatbotConstructorTelegram.Model.StaticData
{
    internal static class TerminalCommands
    {
        public static readonly string StartPythonFile =
                 "cd " + "%PATH%" + "\n" +
                 "python %NAME%.py";

        public static readonly string CommandsActivateEnvironment =
            "chcp 1251\n" + "cd "  + "%PATH%"+"\n" + "\\Scripts\\activate";

    }
}
