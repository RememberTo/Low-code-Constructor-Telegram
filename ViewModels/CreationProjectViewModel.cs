using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.ViewModels.Base;
using Microsoft.WindowsAPICodePack.Dialogs;
using NLog;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using ChatbotConstructorTelegram.View.Window;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class CreationProjectViewModel : ViewModel
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        private readonly DispatcherTimer _timer;

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

        private string? _textNameProject = string.Empty;
        public string? TextNameProject
        {
            get => _textNameProject;
            set => Set(ref _textNameProject, value);
        }

        private string? _textPath = string.Empty;
        public string? TextPath
        {
            get => _textPath;
            set => Set(ref _textPath, value);
        }

        private string? _textToken = string.Empty;
        public string? TextToken
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

        public event EventHandler RequestClose;

        public ICommand CreateProject { get; }

        private bool CanCreateProjectCommandExecute(object p)
        {
            return true;
        }

        private void OnCreateProjectCommandExecuted(object p)
        {

            if (Directory.Exists(TextPath) && TextToken != string.Empty && TextNameProject != string.Empty)
            {
                Log.Info("Введенные данные успешно сохранены");
                DataProject.Instance.PathDirectory = TextPath;
                DataProject.Instance.Path = TextPath + "\\" + TextNameProject+".xml";
                DataProject.Instance.Name = TextNameProject;
                DataProject.Instance.Token = TextToken;
                WriteProjectInList();
                CreateFileProjectAsync();

                var projWnd = new ProjectWindow();
              
                projWnd.Show();
            }
            else
            {
                TextVisibilityWarning = Visibility.Visible;
                _timer.Start();

                if (!Directory.Exists(TextPath))
                    TextWarning = "Введенной директории не существует";
                else
                    TextWarning = "Заполните все поля";

                Log.Error("Введенные данные не сохранены, неправильно заполнены поля");
            }

            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TextVisibilityWarning = Visibility.Hidden;
        }

        private void WriteProjectInList()
        {
            var note = "$" + DataProject.Instance.PathDirectory + "\\" + DataProject.Instance.Name + ".xml" + $"%{DateTime.Now}%";
            FileProjectManager.AppendNoteInFileListProject(ExplorerManager.LocationListProjects, note);
        }

        private async void CreateFileProjectAsync()
        {
            await using var fs = new FileStream(DataProject.Instance.PathDirectory + "\\" + DataProject.Instance.Name + ".xml", FileMode.Create);
        }

        public CreationProjectViewModel()
        {
            #region Command

            ChoseFolder = new LambdaCommand(OnChoseFolderCommandExecuted, OnChoseFolderCommandExecute);
            CreateProject = new LambdaCommand(OnCreateProjectCommandExecuted, CanCreateProjectCommandExecute);
            #endregion

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 5);
        }
    }
}
