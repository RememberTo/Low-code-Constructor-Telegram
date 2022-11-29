using ChatbotConstructorTelegram.Infastructure.Commands;
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
using ChatbotConstructorTelegram.Infastructure;


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

        #region ResizeMenuCommand
        public ICommand ResizeMenuCommand { get; }

        private bool OnResizeMenuCommandExecute(object p)
        {
            return true;
        }

        private void CanResizeMenuCommandExecuted(object p)
        {
            //if (WidthMenu != 150)
            //    WidthMenu = 150;
            //else
            //    this.WidthMenu = 50;
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

            ResizeMenuCommand = new LambdaCommand(CanResizeMenuCommandExecuted,OnResizeMenuCommandExecute);
            CloseApplicationCommand =
                new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandExecute);

            #endregion

        }

    }
}
