using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.View.Window;
using ChatbotConstructorTelegram.ViewModels.Base;
using NLog;

using System;
using System.Windows;
using System.Windows.Input;
using ChatbotConstructorTelegram.Model.Bot;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    internal class NoteProject : ViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public string? ProjectName { get; set; }
        public string? Path { get; set; }
        public DateTime Date { get; set; }

        public ICommand OpenProjectCommand { get; }

        private bool OnOpenProjectCommandExecute(object p)
        {
            return System.IO.File.Exists((string)p);
        }

        private void OnOpenProjectCommandExecuted(object p)
        {
            try
            {
                var path = (string)p;
                DataProject.Name = ProjectName;
                DataProject.Path = Path;
                DataProject.PathDirectory = System.IO.Path.GetDirectoryName(Path);
                var pWnd = new ProjectWindow(path);
                pWnd.Show();
                Logger.Info($"Проект {path} передан в окно конструктора");
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
                MessageBox.Show(exception.Message);

            }
        }

        public NoteProject()
        {
            OpenProjectCommand = new LambdaCommand(OnOpenProjectCommandExecuted, OnOpenProjectCommandExecute);
        }
    }
}
