using System.IO;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using NLog;
using System;
using System.Xml.Serialization;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.StaticData;

namespace ChatbotConstructorTelegram.Model.WorkEnvironment
{
    internal class RuntimeSystemManager
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        public static IRuntimeStatusSystem? CheckSystem()
        {
            try
            {
                //var are = new RuntimeSystem();
                //SaveConfigSystem(are);
                //return null;
                var runtimeSystem = GetConfigSystem();
                if (runtimeSystem == null) throw new ArgumentNullException("runtimeSystem is Null");

                if (runtimeSystem.PythonInfo is { IsInstalled: false })
                    runtimeSystem.PythonInfo.CheckPythonAndPip();

                if (runtimeSystem.PythonInfo != null && CheckPythonAndPipVersion(runtimeSystem.PythonInfo))
                    throw new WorkEnvironmentException("Необходимые версии:\npython 3.9.X-3.10.X\npip 22.X.X");

                runtimeSystem.IsExistPython = true;

                if (runtimeSystem.IsExistEnvironment == false)
                {
                    runtimeSystem.Environment?.CreateEnv();

                    runtimeSystem.IsExistEnvironment = true;
                }

                if (runtimeSystem.IsExistLibrary == false)
                {
                    PythonLibraryInstaller.InstallLibrary(ExplorerManager.LocationArchiveAiogram,
                        ExplorerManager.NameArchiveAiogram, runtimeSystem.Environment.Path);

                    if (runtimeSystem.PythonLibraryInfo != null)
                    {
                        runtimeSystem.PythonLibraryInfo.NameLibrary = "aiogram";
                        if (runtimeSystem.Environment != null)
                            runtimeSystem.PythonLibraryInfo.CheckLibrary(runtimeSystem.Environment.Path);

                        if (runtimeSystem.PythonLibraryInfo.IsInstalled)
                            runtimeSystem.IsExistLibrary = true;
                    }
                }

                DataProject.Instance.PathEnvironment = runtimeSystem.Environment.Path;

                SaveConfigSystem(runtimeSystem);

                return (IRuntimeStatusSystem)runtimeSystem;
            }
            catch (Exception e)
            {
                throw new WorkEnvironmentException(e.Message);
            }
        }

        private static bool CheckPythonAndPipVersion(PythonInformation info)
        {
            var isPythonVersion = true;
            var isPipVersion = true;

            if (info.Version.Contains("3.9") || info.Version.Contains("3.10"))
                isPythonVersion = false;
            if (info.PipVersion.Contains("22") || info.PipVersion.Contains("23"))
                isPipVersion = false;

            return isPythonVersion && isPipVersion;
        }

        private static async void SaveConfigSystem(RuntimeSystem sys)
        {
            var xmlSerializer = new XmlSerializer(typeof(RuntimeSystem));

            await using var fs = new FileStream(ExplorerManager.LocationWorkEnvironment, FileMode.Truncate);

            xmlSerializer.Serialize(fs, sys);

            Logger.Info("Статус рабочей среды сериализован");
        }

        private static RuntimeSystem? GetConfigSystem()
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(RuntimeSystem));
                using var fs = System.IO.File.OpenRead(ExplorerManager.LocationWorkEnvironment);
                var sys = xmlSerializer.Deserialize(fs) as RuntimeSystem;
                return sys;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }
    }
}
