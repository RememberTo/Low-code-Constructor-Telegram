using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using NLog;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    [DataContract]
    public class Environment
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string Name { get; set; }
        

        public Environment()
        {

        }

        public void CreateEnv()
        {
            Name = ".bot";
            Path = ExplorerManager.LocationEnv+"\\.bot";

            string createEnvCommand = "python -m venv " + Name;
            

            Logger.Info("Creating virtual environment with command: " + createEnvCommand);

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
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
                        writer.WriteLine("cd "+ ExplorerManager.LocationEnv);
                        writer.WriteLine(createEnvCommand);
                    }
                }
            }
            Logger.Info("Virtual environment created successfully");
        }

    }
}
