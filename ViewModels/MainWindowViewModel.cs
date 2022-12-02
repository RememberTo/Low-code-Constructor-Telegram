using System;
using System.Collections.Generic;
using System.Configuration;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.View.Window;
using ChatbotConstructorTelegram.ViewModels.Base;
using ChatbotConstructorTelegram.Model.Bot;
using System.Windows;
using System.Windows.Input;
using ChatbotConstructorTelegram.Model.ViewData;
using NLog;


namespace ChatbotConstructorTelegram.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private string _title = "Конструктор бота";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _status = "Готово";

        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        private List<RecentProject> _recentProjects;

        public List<RecentProject> RecentProjects
        {
            get => _recentProjects;
            set => Set(ref _recentProjects, value);
        }
        #region Command

        public ICommand CloseApplicationCommand { get; }

        private bool OnCloseApplicationCommandExecute(object p)
        {
            return true;
        }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        public MainWindowViewModel()
        {
            #region Command

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion

            RecentProjects = new List<RecentProject>()
            {
                new RecentProject() { ProjectName = "Hello", Path = @"C:\\Koren\Tut\Lezit\Files\a\Project", Date = DateTime.Now},
                new RecentProject() { ProjectName = "Eazy", Path = @"C:\Koren\delo", Date = DateTime.MinValue},
            };

            Log.Info("Главное окно успешно запущено");
        }

    }
}
