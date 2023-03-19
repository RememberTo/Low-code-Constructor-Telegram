using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    [DataContract]
    public class PythonInformation
    {
        [DataMember]
        public bool? IsInstalled { get; set; }

        [DataMember]
        public string? Version { get; set; }

        [DataMember]
        public string? PipVersion { get; set; }

        public PythonInformation()
        {
            
        }

        public void CheckPythonAndPip()
        {
            var process = new Process();
            process.StartInfo.FileName = "python";
            process.StartInfo.Arguments = "-V";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            try
            {
                process.Start();
                process.WaitForExit();
                var pythonVersion = process.StandardOutput.ReadToEnd().Trim();

                Version = pythonVersion;
                IsInstalled = true;
            }
            catch (Exception)
            {
                IsInstalled = false;
                Version = "Python None";
                throw new WorkEnvironmentException("Python is not installed");
            }

            process.StartInfo.Arguments = "-m pip show pip";
            try
            {
                process.Start();
                process.WaitForExit();
                var pipVersion = process.StandardOutput.ReadToEnd().Trim();

                PipVersion = pipVersion;
            }
            catch (Exception)
            {
                PipVersion = "Pip None";
                throw new WorkEnvironmentException("Pip is not installed");
            }
        }
    }
}
