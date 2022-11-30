using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChatbotConstructorTelegram.Infrastructure;
using ChatbotConstructorTelegram.View.Window;


namespace TaskList.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        private string _title = "Список задач";
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

        #region Command

        #region CreateBotCommand
        public ICommand CreateBotCommand { get; }


        private bool OnCreateBotCommandExecute(object p)
        {
            return true;
        }

        private void CanCreateBotCommandExecuted(object p)
        {
            var start = new BotCreationWindow();
            var dialogResult = start.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    Status = "Норм";
                    break;
                case false:
                    Status = "Окно закрыто";
                    break;
                default:
                    // Indeterminate
                    break;
            }
        }

        #endregion

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

            CreateBotCommand = new LambdaCommand(CanCreateBotCommandExecuted, OnCreateBotCommandExecute);
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion

        }

    }
}
