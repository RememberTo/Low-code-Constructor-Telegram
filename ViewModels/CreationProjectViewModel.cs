using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.ViewModels.Base;
using Microsoft.WindowsAPICodePack.Dialogs;
using NLog;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class CreationProjectViewModel : ViewModel
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private string _textWarning = string.Empty;
        public string TextWarning
        {
            get => _textWarning;
            set => Set(ref _textWarning, value);
        }

        private Visibility _textVisibilityWarning = Visibility.Hidden;
        public Visibility TextVisibilityWarning
        {
            get => _textVisibilityWarning;
            set => Set(ref _textVisibilityWarning, value);
        }

        private string _textNameProject = string.Empty;
        public string TextNameProject
        {
            get => _textNameProject;
            set => Set(ref _textNameProject, value);
        }

        private string _textPath = string.Empty;
        public string TextPath
        {
            get => _textPath;
            set => Set(ref _textPath, value);
        }

        private string _textToken = string.Empty;
        public string TextToken
        {
            get => _textToken;
            set => Set(ref _textToken, value);
        }

        #region Command
        public ICommand ChoseFolder { get; }

        private bool OnChoseFolderCommandExecute(object p)
        {
            return true;
        }

        private void OnChoseFolderCommandExecuted(object p)
        {
            try
            {
                CommonOpenFileDialog ofd = new() { IsFolderPicker = true };
                if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    TextPath = ofd.FileName;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public ICommand CreateProject { get; }

        private bool CanCreateProjectCommandExecute(object p)
        {
            return true;
        }

        private void OnCreateProjectCommandExecuted(object p)
        {
            Token = TextToken;
            NameProject = TextNameProject;
            PathDirectory = TextPath;

            if (Directory.Exists(PathDirectory) && Token != string.Empty && NameProject != string.Empty)
            {
                Log.Info("Введенные данные успешно сохранены");
                StaticDataBot.PathDirectory = PathDirectory;
                StaticDataBot.Name = NameProject;
                StaticDataBot.Token = Token;
            }
            else
            {
                TextVisibilityWarning = Visibility.Visible;
                _timer.Start();

                if (!Directory.Exists(PathDirectory))
                    TextWarning = "Введенной директории не существует";
                else
                    TextWarning = "Заполните все поля";

                Log.Error("Введенные данные не сохранены, неправильно заполнены поля");
            }
        }

        #endregion

        public string PathDirectory { get; private set; }
        public string Token { get; private set; }
        public string NameProject { get; private set; }
        private DispatcherTimer _timer;



        private void timer_Tick(object sender, EventArgs e)
        {
            TextVisibilityWarning = Visibility.Hidden;
        }

        public CreationProjectViewModel()
        {
            #region Command

            ChoseFolder = new LambdaCommand(OnChoseFolderCommandExecuted, OnChoseFolderCommandExecute);
            CreateProject = new LambdaCommand(OnCreateProjectCommandExecuted, CanCreateProjectCommandExecute);
            #endregion

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 5);
        }
    }
}
