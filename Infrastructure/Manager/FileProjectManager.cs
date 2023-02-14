using ChatbotConstructorTelegram.Model.File;
using NLog;
using System;
using System.IO;
using System.Xml.Serialization;
using ChatbotConstructorTelegram.Model.Bot;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace ChatbotConstructorTelegram.Infrastructure.Manager
{
    internal class FileProjectManager
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        public static async void CreateAndSaveFileSettingsAsync(WrapperDataBot wrapper)
        {
            var xmlSerializer = new XmlSerializer(typeof(WrapperDataBot));

            if (wrapper.DataProject.Path != null)
            {
                await using var fs = new FileStream(wrapper.DataProject.Path, FileMode.Truncate);

                xmlSerializer.Serialize(fs, wrapper);

                Logger.Info("Проект сериализован");
            }
        }

        public static WrapperDataBot? GetWrapperDataBot(string pathFile)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(WrapperDataBot));
                using var fs = File.OpenRead(pathFile);
                var dataBot = xmlSerializer.Deserialize(fs) as WrapperDataBot;
                return dataBot;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public static async void AppendNoteInFileListProject(string pathFile, string note)
        {
            await using var output = new StreamWriter(new FileStream(pathFile, FileMode.Append));
            await output.WriteLineAsync(note);
        }
    }
}
