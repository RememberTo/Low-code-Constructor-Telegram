using ChatbotConstructorTelegram.Infrastructure.Manager;

namespace ChatbotConstructorTelegram.Model.StaticData
{
    internal static class TerminalCommands
    {
        public static readonly string StartPythonFile =
                 "\"%PATHENV%\\Scripts\\python.exe\" " + "\"%PATH%" + "\\%NAME%.py\"";

        public static readonly string CommandsActivateEnvironment =
            "chcp 1251\n" + "cd "  + "%PATH%\\Scripts" + "\n" + "activate";

    }
}
