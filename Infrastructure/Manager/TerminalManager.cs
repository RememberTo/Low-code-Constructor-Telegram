using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ChatbotConstructorTelegram.Infrastructure.Manager
{
    internal class TerminalManager
    {
        public static void ExecuteConsoleCommand(string commands)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                }
            };

            process.Start();

            using var writer = process.StandardInput;
            if (!writer.BaseStream.CanWrite) return;
            foreach (var line in commands.Split('\n'))
            {
                writer.WriteLine(line);
                System.Threading.Thread.Sleep(3000);
            }
        }

        public static StreamWriter ExecuteConsoleCommand(Process process, string commands)
        {
            var writer = process.StandardInput;
            if (!writer.BaseStream.CanWrite) return null;
            foreach (var line in commands.Split('\n'))
            {
                writer.WriteLine(line);
               // RuntimeSystem.Threading.Thread.Sleep(3000);
            }

            return writer;
        }

        public static void StartPythonFile(string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                }
            };
            process.Start();

        }

        public static void ExecuteConsoleCommand(List<string> commands)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false

                }
            };
            process.Start();

            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    foreach (var command in commands)
                        writer.WriteLine(command);
                }
            }
        }
    }
}
