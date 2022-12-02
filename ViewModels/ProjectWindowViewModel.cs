using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.View.ModalWindow;
using ChatbotConstructorTelegram.ViewModels.Base;
using NLog;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class ProjectWindowViewModel : ViewModel
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        private DispatcherTimer _timer;

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

        private string _textChangeToken = "Изменить токен";

        public string TextChangeToken
        {
            get => _textChangeToken;
            set => Set(ref _textChangeToken, value);
        }

        #region Command
        public ICommand ChangeTokenCommand { get; }

        private bool CanChangeTokenCommandExecute(object p)
        {
            return true;
        }

        private void OnChangeTokenCommandExecuted(object p)
        {
            var changeTokenWnd = new ChangeTokenModalWindow();
            var resultDialog =  changeTokenWnd.ShowDialog();

            switch (resultDialog)
            {
                case true:
                    Status = "Токен изменен";
                    _timer.Start();
                    break;
                case false:
                    Status = "Токен не изменен";
                    _timer.Start();
                    break;
                default:
                    Status = "Ошибка";
                    _timer.Start();
                    break;
            }
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            Status = "Готово";
        }

        public ProjectWindowViewModel()
        {
            #region Command

            ChangeTokenCommand = new LambdaCommand(OnChangeTokenCommandExecuted, CanChangeTokenCommandExecute);

            #endregion
            //Log.Info("Inicialization");
            //Log.Trace("Trace");
            //Log.Warn("Warn");
            //Log.Error("Erorr");
            //Log.Fatal("Fatal");
            //Log.Debug("Debug");

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 5);
        }
    }
}
