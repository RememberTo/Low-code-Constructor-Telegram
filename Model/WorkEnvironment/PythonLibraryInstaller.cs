using System;
using System.Diagnostics;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    public static class PythonLibraryInstaller
    {
        public static void InstallLibrary(string libraryArchivePath, string libraryNameFile, string envPath)
        {
            try
            {
                string command = $"pip install {libraryNameFile}";
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                };

                using (var process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    using (var writer = process.StandardInput)
                    {
                        if (writer.BaseStream.CanWrite)
                        {
                            writer.WriteLine("chcp 1251");
                            writer.WriteLine("cd " + envPath+"\\Scripts");
                            writer.WriteLine("activate");
                            writer.WriteLine("cd "+ libraryArchivePath);
                            writer.WriteLine(command);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new WorkEnvironmentException("Библиотека aiogram не установлена");
            }
            
        }

        private static string GetPythonExecutablePath()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "where",
                    Arguments = "python",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            var pythonPath = process.StandardOutput.ReadLine();
            process.WaitForExit();
            return pythonPath;
        }
    }
}
