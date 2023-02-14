using System;
using System.Diagnostics.CodeAnalysis;

namespace ChatbotConstructorTelegram.Model.Bot
{
    public class DataProject
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Token { get; set; }
        public string? PathDirectory { get; set; }
        public string? Path { get; set; }
        public string? PathLastPythonFile { get; set; }
        public string? PathEnvironment { get; set; } 

        public bool IsReadyAiogram { get; set; }
        public bool IsReadyPython { get; set; }

        static DataProject _instance = new DataProject();

        public static DataProject Instance
        {
            get => _instance;
            set => _instance = value;
        }
        public DataProject()
        {
            
        }
    }
}
