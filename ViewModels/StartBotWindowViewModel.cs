using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.StaticData;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.ViewModels.Base;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class StartBotWindowViewModel : ViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private string _title = DataProject.Instance.Name ?? string.Empty;

        private Process cmdProcess = null!;

        public bool IsPool { get; private set; } = true;

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

        private Visibility _visibilityLoadGrid = Visibility.Collapsed;
        public Visibility VisibilityLoadGrid
        {
            get => _visibilityLoadGrid;
            set => Set(ref _visibilityLoadGrid, value);
        }

        private double _opacityGridInfo = 1;
        public double OpacityGridInfo
        {
            get => _opacityGridInfo;
            set => Set(ref _opacityGridInfo, value);
        }

        private bool _isEnableButtonStart = true;
        public bool IsEnableButtonStart
        {
            get => _isEnableButtonStart;
            set => Set(ref _isEnableButtonStart, value);
        }

        private bool _isEnableButtonStop = true;
        public bool IsEnableButtonStop
        {
            get => _isEnableButtonStop;
            set => Set(ref _isEnableButtonStop, value);
        }


        #region Command

        public ICommand? StartPollingCommand { get; private set; }

        private async void OnStartPollingCommandExecuted(object p)
        {
            try
            {
                cmdProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                EnabledButtons(false);

                cmdProcess.Start();
                Logger.Info("Процесс запущен");
                Messages.AddInfo("Запуск бота");

                var a = TerminalCommands.CommandsActivateEnvironment.Replace("%PATH%", DataProject.Instance.PathEnvironment);
                var b = TerminalCommands.StartPythonFile.Replace("%PATHENV%", DataProject.Instance.PathEnvironment).Replace("%PATH%", DataProject.Instance.PathDirectory).Replace("%NAME%", DataProject.Instance.Name);

                await Task.Run(() => InputCommandsTerminal(TerminalCommands.CommandsActivateEnvironment
                                                               .Replace("%PATH%", DataProject.Instance.PathEnvironment) 
                                                           + "\n" + TerminalCommands.StartPythonFile
                                                               .Replace("%PATHENV%", DataProject.Instance.PathEnvironment)
                                                               .Replace("%PATH%", DataProject.Instance.PathDirectory)
                                                               .Replace("%NAME%", DataProject.Instance.Name)));

                await Task.Run(StartPulling);

                Messages.AddInfo("Бот запущен");
                EnabledButtons(true);

                IsPool = true;

            }
            catch (Exception)
            {
                Messages.AddInfo("Ошибка, завершение работы бота");
                Logger.Error("Ошибка, поптыка остановить пуллинг");
                StopPollingCommand?.Execute(null);
            }
        }
        private bool CanStartPollingCommandExecute(object p)
        {
            return !IsPool;
        }

        public ICommand? StopPollingCommand { get; private set; }

        private async void OnStopPollingCommandExecuted(object p)
        {
            try
            {
                if (!IsPool) return;

                IsPool = false;
                VisibilityLoadGrid = Visibility.Visible;
                OpacityGridInfo = 0.3;
                EnabledButtons(false);

                InputCommandsTerminal("exit");

                await Task.Run(() => StopPulling());

                VisibilityLoadGrid = Visibility.Collapsed;
                OpacityGridInfo = 1;
                EnabledButtons(true);

                Messages.AddInfo("Бот выключен");
                Logger.Error("Пуллинг остановлен");
            }
            catch (Exception)
            {
                Messages.AddInfo("Ошибка при отключении бота");
                Logger.Error("Пуллинг не удалось остановить");
            }
            finally
            {
                cmdProcess.Close();
                cmdProcess.Dispose();

            }
        }
        private bool CanStopPollingCommandExecute(object p)
        {
            return IsPool;
        }

        #endregion

        public ObservableCollection<ConsoleDataView> Messages { get; set; }

        private void InputCommandsTerminal(string commands)
        {
            foreach (var line in commands.Split('\n'))
            {
                cmdProcess.StandardInput.WriteLine(line);
            }
        }

        private void StopPulling()
        {
            var line = cmdProcess.StandardOutput.ReadLine();
        }

        private void StartPulling()//Что будет если не придет BotCodeGenerator start и будет бесконечные цикл
        {
            var line = cmdProcess.StandardOutput.ReadLine();
            while (line != null && !line.Contains("Bot start"))
            {
                line = cmdProcess.StandardOutput.ReadLine();
            }
        }

        private void InitializationCommand()
        {
            StartPollingCommand = new LambdaCommand(OnStartPollingCommandExecuted, CanStartPollingCommandExecute);
            StopPollingCommand = new LambdaCommand(OnStopPollingCommandExecuted, CanStopPollingCommandExecute);
        }

        private void EnabledButtons(bool status)
        {
            IsEnableButtonStart = status;
            IsEnableButtonStop = status;
        }

        public StartBotWindowViewModel()
        {
            InitializationCommand();

            Messages = new ObservableCollection<ConsoleDataView>();

            Logger.Info("Начальные данные проинициализированны");
            StartPollingCommand?.Execute(null);
        }
    }

    static class ListExtensions
    {
        public static void AddInfo(this ObservableCollection<ConsoleDataView> list, string item)
        {
            if (string.IsNullOrEmpty(item)) item = "-";

            var cdw = new ConsoleDataView
            {
                Info = item,
                Time = DateTime.Now,
            };

            list.Add(cdw);
        }
    }
}
