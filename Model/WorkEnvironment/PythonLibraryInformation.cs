using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    [DataContract]
    public class PythonLibraryInformation
    {
        [DataMember]
        public bool IsInstalled { get; set; }

        [DataMember]
        public string? NameLibrary { get; set; }
        
        [DataMember]
        public string? Version { get; set; }

        public PythonLibraryInformation(string env, string nameLibrary)
        {
            //NameLibrary = nameLibrary;
            //CheckLibrary(env);
        }

        public PythonLibraryInformation()
        {
            //NameLibrary = nameLibrary;
            //CheckLibrary(env);
        }

        public void CheckLibrary(string env)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
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
                            writer.WriteLine("cd " + env + "\\Scripts");
                            writer.WriteLine("activate");
                            writer.WriteLine("pip list");
                        }
                    }

                    var output = process.StandardOutput.ReadToEnd();

                    if (output.Contains(NameLibrary ?? throw new InvalidOperationException()))
                    {
                        IsInstalled = true;
                        foreach (var item in output.Split('\r'))
                        {
                            if (item.Contains(NameLibrary))
                            {
                                Version = item.Split(' ')[^1];
                            }
                        }
                    }
                    else
                        IsInstalled = false;
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
