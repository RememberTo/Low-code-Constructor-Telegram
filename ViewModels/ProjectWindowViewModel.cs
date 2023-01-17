﻿using ChatbotConstructorTelegram.Infrastructure;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Infrastructure.Manager;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.File;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.View.ModalWindow;
using ChatbotConstructorTelegram.View.Window;
using ChatbotConstructorTelegram.ViewModels.Base;
using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class ProjectWindowViewModel : ViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DispatcherTimer _timer;

        #region Binding Params

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

        #region Document



        #endregion

        #region Photo



        #endregion

        public ObservableCollection<IPropertyBot> BotCommands { get; set; }

        private BotCommandProperty? _botCommand;

        public BotCommandProperty? BotCommand
        {
            get => _botCommand;
            set => Set(ref _botCommand, value);
        }

        private BotTextProperty? _botTextProperty;

        public BotTextProperty? BotTextProperty
        {
            get => _botTextProperty;
            set => Set(ref _botTextProperty, value);
        }

        private IPropertyBot? _selectedCommand;

        public IPropertyBot? SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                if (value is BotTextProperty property)
                {
                    BotTextProperty = property;
                    VisibilityBotCommand = Visibility.Collapsed;
                    VisibilityBotText = Visibility.Visible;
                }
                else if (value is BotCommandProperty commandProperty)
                {
                    BotCommand = commandProperty;
                    VisibilityBotCommand = Visibility.Visible;
                    VisibilityBotText = Visibility.Collapsed;
                }
                else
                {
                    BotCommand = null;
                    BotTextProperty = null;
                    VisibilityBotCommand = Visibility.Collapsed;
                    VisibilityBotText = Visibility.Collapsed;
                }
                Set(ref _selectedCommand, value);
            }
        }

        private Visibility _visibilityBotCommand;
        public Visibility VisibilityBotCommand
        {
            get => _visibilityBotCommand;
            set => Set(ref _visibilityBotCommand, value);
        }

        Visibility _visibilityBotText;
        public Visibility VisibilityBotText
        {
            get => _visibilityBotText;
            set => Set(ref _visibilityBotText, value);
        }

        #endregion

        #region Command

        public ICommand? CloseApplicationCommand { get; private set; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            var dialogBox = new QuestionSaveProject();

            var dialogResult = dialogBox.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    if (dialogBox.IsSaveAsFile)
                        SaveProjectCommand?.Execute(null); // Сохраняем модель в файл
                                                           // Не сохраняем
                    Application.Current.Shutdown();
                    break;
                case false:
                    break;
                default:
                    Logger.Error("Не удалось сохранить файл");
                    MessageBox.Show("Ошибка");

                    break;
            }

        }
        private bool CanAlwaysFullCommandExecute(object p)
        {
            return true;
        }

        public ICommand? OpenProjectCommand { get; private set; }
        private void OnOpenProjectCommandExecuted(object p)
        {
            try
            {
                SetStatusStartTimer("Открытие проекта");

                var ofd = new OpenFileDialog
                {
                    Filter = "Файлы проекта (*.xml)|*.xml;*"
                };

                if (ofd.ShowDialog() != true) return;

                var pWnd = new ProjectWindow(ofd.FileName);
                pWnd.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        public ICommand? SaveProjectCommand { get; private set; }
        private void OnSaveProjectCommandExecuted(object p)
        {
            FileProjectManager.CreateAndSaveFileSettingsAsync(new WrapperDataBot(BotCommands));

            if (File.Exists(DataProject.PathDirectory + "\\" + DataProject.Name + ".xml"))
                SetStatusStartTimer("Проект сохранен");
            else
                SetStatusStartTimer("Не удалось сохранить файл");
        }

        public ICommand? CreateProjectCommand { get; private set; }
        private void OnCreateProjectCommandExecuted(object p)
        {
            var crWnd = new CreationProjectWindow();
            SetStatusStartTimer("Создание проекта");
            crWnd.Show();
        }

        public ICommand? CreateBotCommand { get; private set; }


        private void OnCreateBotCommandExecuted(object p)
        {
            var param = new ParametersBot();
            var bot = new Bot(BotCommands, param);
            bot.CreateBot();
        }

        public ICommand? ChangeTokenCommand { get; private set; }

        private void OnChangeTokenCommandExecuted(object p)
        {
            var changeTokenWnd = new ChangeTokenModalWindow();
            var resultDialog = changeTokenWnd.ShowDialog();

            switch (resultDialog)
            {
                case true:
                    SetStatusStartTimer("Токен изменен");
                    break;
                case false:
                    SetStatusStartTimer("Токен не изменен");
                    break;
                default:
                    SetStatusStartTimer("Ошибка");
                    break;
            }
        }

        public ICommand? AddCommand { get; private set; }

        private void OnAddCommandExecuted(object p)
        {
            BotCommands.Add(new BotCommandProperty() { Id = 0, Description = "", Name = "newCommand" });
            SetStatusStartTimer("Команда добавлена");
        }


        public ICommand? AddTextCommand { get; private set; }

        private void OnAddTextCommandExecuted(object p)
        {
            BotCommands.Add(new BotTextProperty() { Name = "Text" });
        }

        public ICommand? DeleteCommand { get; private set; }

        private bool CanDeleteCommandExecute(object p)
        {
            return p is BotCommandProperty cmd && BotCommands.Contains(cmd);
        }

        private void OnDeleteCommandExecuted(object p)
        {
            if (p is not IPropertyBot cmd) return;
            SetStatusStartTimer("Элемент " + cmd.Name + " удален");
            BotCommands.Remove(cmd);
            SelectedCommand = null;
            SetStatusStartTimer("");
        }

        public ICommand? ChooseFileBotCommand { get; private set; }

        private void OnChooseFileBotCommandExecuted(object p)
        {
            try
            {
                var ofd = new OpenFileDialog();

                if (ofd.ShowDialog() != true) return;
                if (BotCommand != null)
                    BotCommand.Document.Path = ofd.FileName;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public ICommand? ChooseImageBotCommand { get; private set; }

        private void OnChooseImageBotCommandExecuted(object p)
        {
            try
            {
                var ofd = new OpenFileDialog()
                {
                    Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png",
                };

                if (ofd.ShowDialog() == true)
                {
                    if (BotCommand != null)
                        BotCommand.Photo.Path = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Status = "Готово";
        }

        private void SetStatusStartTimer(string s)
        {
            Status = s;
            _timer.Start();
        }

        private void InitializationCommand()
        {
            ChangeTokenCommand = new LambdaCommand(OnChangeTokenCommandExecuted, CanAlwaysFullCommandExecute);
            AddCommand = new LambdaCommand(OnAddCommandExecuted, CanAlwaysFullCommandExecute);
            AddTextCommand = new LambdaCommand(OnAddTextCommandExecuted, CanAlwaysFullCommandExecute);
            CreateBotCommand = new LambdaCommand(OnCreateBotCommandExecuted, CanAlwaysFullCommandExecute);
            ChooseFileBotCommand = new LambdaCommand(OnChooseFileBotCommandExecuted, CanAlwaysFullCommandExecute);
            ChooseImageBotCommand = new LambdaCommand(OnChooseImageBotCommandExecuted, CanAlwaysFullCommandExecute);
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanAlwaysFullCommandExecute);
            DeleteCommand = new LambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            CreateProjectCommand = new LambdaCommand(OnCreateProjectCommandExecuted, CanAlwaysFullCommandExecute);
            SaveProjectCommand = new LambdaCommand(OnSaveProjectCommandExecuted, CanAlwaysFullCommandExecute);
            OpenProjectCommand = new LambdaCommand(OnOpenProjectCommandExecuted, CanAlwaysFullCommandExecute);
        }



        public ProjectWindowViewModel()
        {
            #region Inicialization
            //VisibilityBotCommand = Visibility.Hidden;
            //VisibilityBotText = Visibility.Hidden;
            BotCommands = new ObservableCollection<IPropertyBot>() { new BotCommandProperty() { Id = 0, Description = "Начальная команда", Name = "start" } };

            #endregion

            #region Command

            InitializationCommand();

            #endregion

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 5);
        }

        public ProjectWindowViewModel(string path)
        {
            #region Inicialization
            //VisibilityBotCommand = Visibility.Hidden;
            //VisibilityBotText = Visibility.Hidden;
            WrapperDataBot? wrapperDataBot = FileProjectManager.GetWrapperDataBot(path);

            if (wrapperDataBot != null)
            {
                DataProject.PathDirectory = wrapperDataBot.PathDirectory;
                DataProject.Name = wrapperDataBot.Name;
                DataProject.Description = wrapperDataBot.Description;
                DataProject.Token = wrapperDataBot.Token;
                DataProject.IsReadyAiogram = wrapperDataBot.IsReadyAiogram;
                DataProject.IsReadyPython = wrapperDataBot.IsReadyPython;
                var list = new List<IPropertyBot>();
                if (wrapperDataBot.CommandProperties != null) list.AddRange(wrapperDataBot.CommandProperties);
                if (wrapperDataBot.TextProperties != null) list.AddRange(wrapperDataBot.TextProperties);
                BotCommands = new ObservableCollection<IPropertyBot>(list);
            }
            else
            {
                BotCommands = new ObservableCollection<IPropertyBot>();
            }

            #endregion

            #region Command

            InitializationCommand();

            #endregion

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 5);
        }
    }
}
