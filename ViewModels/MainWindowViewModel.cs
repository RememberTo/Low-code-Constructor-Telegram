using System.Configuration;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.View.Window;
using ChatbotConstructorTelegram.ViewModels.Base;
using ChatbotConstructorTelegram.Model.Bot;
using System.Windows;
using System.Windows.Input;


namespace ChatbotConstructorTelegram.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        #region Private Property

        private Bot _bot;
        #endregion

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

        #region Command

        #region CreateBotCommand
        public ICommand CreateBotCommand { get; }


        private bool OnCreateBotCommandExecute(object p)
        {
            return true;
        }

        private void CanCreateBotCommandExecuted(object p)
        {
            var createBotWnd = new BotCreationWindow();
            var dialogResult = createBotWnd.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    Status = "Создание проекта";
                    
                    _bot = new Bot()
                    {
                        Name = createBotWnd.NameProject, 
                        PathDirectory = createBotWnd.PathDirectory, 
                        Token = createBotWnd.Token
                    };

                    break;
                case false:
                    Status = "Отмена";
                    break;
                default:
                    Status = "Ошибка";
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
