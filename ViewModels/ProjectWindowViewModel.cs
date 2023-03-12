using ChatbotConstructorTelegram.Infrastructure;
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
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class ProjectWindowViewModel : ViewModel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DispatcherTimer _timer;

        public bool IsCancel = false;

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

        private string _nameInlineButton = "Hello";

        public string NameInlineButton
        {
            get => _nameInlineButton;
            set => Set(ref _nameInlineButton, value);
        }

        private string _nameMarkupButton = "Hello";

        public string NameMarkupButton
        {
            get => _nameMarkupButton;
            set => Set(ref _nameMarkupButton, value);
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

        private InlineButtonProperty? _botInlineButtonProperty;

        public InlineButtonProperty? BotInlineButtonProperty
        {
            get => _botInlineButtonProperty;
            set => Set(ref _botInlineButtonProperty, value);
        }

        private MarkupButtonProperty? _botMarkupButtonProperty;

        public ICollectionView InlineButtonCollectionView
        {
            get
            {
                var collectionView = CollectionViewSource.GetDefaultView(SelectedCommand.Children);
                collectionView.Filter = item => item is InlineButtonProperty;
                return collectionView;
            }
        }

        public ICollectionView MarkupButtonCollectionView
        {
            get
            {
                var collectionView = CollectionViewSource.GetDefaultView(SelectedCommand.Children);
                collectionView.Filter = item => item is MarkupButtonProperty;
                return collectionView;
            }
        }

        public MarkupButtonProperty? BotMarkupButtonProperty
        {
            get => _botMarkupButtonProperty;
            set => Set(ref _botMarkupButtonProperty, value);
        }

        private IPropertyBot _selectedCommand;

        public IPropertyBot SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                ResetVisibleProperty(value);
                Set(ref _selectedCommand, value);
            }
        }



        private void ResetVisibleProperty(IPropertyBot propertyBot)
        {
            switch (propertyBot)
            {
                case BotCommandProperty botCommandProperty:
                    BotCommand = botCommandProperty;
                    BotTextProperty = null;
                    BotInlineButtonProperty = null;
                    VisibilityBotCommand = Visibility.Visible;
                    VisibilityBotText = Visibility.Collapsed;
                    break;
                case BotTextProperty botTextProperty:
                    BotTextProperty = botTextProperty;
                    BotCommand = null;
                    BotInlineButtonProperty = null;
                    VisibilityBotText = Visibility.Visible;
                    VisibilityBotCommand = Visibility.Collapsed;
                    break;
                case InlineButtonProperty botinlineButtonProperty:
                    BotInlineButtonProperty = botinlineButtonProperty;
                    VisibilityBotCommand = Visibility.Visible;
                    VisibilityBotText = Visibility.Collapsed;
                    break;
                case MarkupButtonProperty botMarkupButtonProperty:
                    BotMarkupButtonProperty = botMarkupButtonProperty;
                    VisibilityBotCommand = Visibility.Visible;
                    VisibilityBotText = Visibility.Collapsed;
                    break;
                default:
                    BotCommand = null;
                    BotTextProperty = null;
                    VisibilityBotCommand = Visibility.Collapsed;
                    VisibilityBotText = Visibility.Collapsed;
                    break;
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
            IsCancel = false;
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
                    IsCancel = true;
                    break;
                default:
                    Logger.Error("Не удалось сохранить файл");
                    MessageBox.Show("Ошибка");

                    break;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        ///
        public ICommand? Test { get; private set; }
        private void OnTestExecuted(object p)
        {


        }
        ///
        ///
        ///
        ///
        /// 
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
            try
            {
                FileProjectManager.CreateAndSaveFileSettingsAsync(new WrapperDataBot(BotCommands));

                if (File.Exists(DataProject.Instance.PathDirectory + "\\" + DataProject.Instance.Name + ".xml"))
                    SetStatusStartTimer("Проект сохранен");
                else
                    SetStatusStartTimer("Не удалось сохранить файл");
            }
            catch (Exception e)
            {
                Logger.Error(e);
                MessageBox.Show(e.Message);
            }
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
            var thread = new Thread(() =>
                    {
                        var bot = new BotCodeGenerator(BotCommands);
                        bot.CreateBot();
                    });
            thread.Start();
            //var startBotWnd = new StartBotWindow();
            //startBotWnd.Show();
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

        public ICommand? AddInlineButtonCommand { get; private set; }

        private void OnAddInlineButtonCommandExecuted(object p)
        {
            try
            {
                if (SelectedCommand == null || SelectedCommand is BotTextProperty)
                    throw new InvalidOperationException();
                var inlineButton = new InlineButtonProperty() { Name = NameInlineButton };
                SelectedCommand.Children.Add(inlineButton);
                Logger.Info($"Inline Кнопка {inlineButton.Name} ID {inlineButton.UniqueId} добавлена ");
                SetStatusStartTimer("Inline кнопка добавлена");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите команду или кнопку!");
            }
             catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public ICommand? AddMarkupButtonCommand { get; private set; }

        private void OnAddMarkupButtonCommandExecuted(object p)
        {
            try
            {
                if (SelectedCommand == null || SelectedCommand is BotTextProperty)
                    throw new InvalidOperationException();
                var markupButton = new MarkupButtonProperty() { Name = NameMarkupButton };
                SelectedCommand.Children.Add(markupButton);
                SetStatusStartTimer("Inline кнопка добавлена");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите команду или кнопку!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
            // Для добавления кнопок можно изменить метод доступа так чтобы если в коллекции 6 элементов нельзя добавлять элементы
            AddInlineButtonCommand = new LambdaCommand(OnAddInlineButtonCommandExecuted, CanAlwaysFullCommandExecute);
            AddMarkupButtonCommand = new LambdaCommand(OnAddMarkupButtonCommandExecuted, CanAlwaysFullCommandExecute);

            Test = new LambdaCommand(OnTestExecuted, CanAlwaysFullCommandExecute);
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
                DataProject.Instance = wrapperDataBot.DataProject;
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
