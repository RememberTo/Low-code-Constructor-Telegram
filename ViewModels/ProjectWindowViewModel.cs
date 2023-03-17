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
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;
using QuickGraph;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;

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

        private string _pathDocument;
        public string PathDocument
        {
            get => _pathDocument;
            set
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите команду или кнопку"); 
                    return;
                }
                if(SelectedCommand.Documents.Count == 0)
                    SelectedCommand.Documents.Add(new Document());
                SelectedCommand.Documents[0].Path=value;
                Set(ref _pathDocument, value);
            }
        }


        private string _captionDocument;
        public string CaptionDocument
        {
            get => _captionDocument;
            set
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите команду или кнопку");
                    return;
                }
                if (SelectedCommand.Documents.Count == 0)
                    SelectedCommand.Documents.Add(new Document());
                SelectedCommand.Documents[0].Caption = value;
                Set(ref _captionDocument, value);
            }
        }

        #endregion

        #region Photo

        private string _captionPhoto;
        public string CaptionPhoto
        {
            get => _captionPhoto;
            set
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите команду или кнопку");
                    return;
                }
                if (SelectedCommand.Photos.Count == 0)
                    SelectedCommand.Photos.Add(new Photo());
                SelectedCommand.Photos[0].Caption = value;
                Set(ref _captionPhoto, value);
            }
        }

        private string _pathPhoto;
        public string PathPhoto
        {
            get => _pathPhoto;
            set
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите команду или кнопку");
                    return;
                }
                if (SelectedCommand.Photos.Count == 0)
                    SelectedCommand.Photos.Add(new Photo());
                SelectedCommand.Photos[0].Path = value;
                Set(ref _pathPhoto, value);
            }
        }

        #endregion

        #region RadioButton

        private bool _atachMarkupButtonMessageDefault;
        public bool AtachMarkupButtonMessageDefault
        {
            get => _atachMarkupButtonMessageDefault;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachMarkupButtonMessage.Default = value;
                Set(ref _atachMarkupButtonMessageDefault, value);
            }
        }

        private bool _atachMarkupButtonMessageText;
        public bool AtachMarkupButtonMessageText
        {
            get => _atachMarkupButtonMessageText;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachMarkupButtonMessage.Text = value;
                Set(ref _atachMarkupButtonMessageText, value);
            }
        }

        private bool _atachMarkupButtonMessagePhoto;
        public bool AtachMarkupButtonMessagePhoto
        {
            get => _atachMarkupButtonMessagePhoto;
            set
            {
                if (SelectedCommand == null)
                    return;
                
                SelectedCommand.AtachMarkupButtonMessage.Photo = value;
                Set(ref _atachMarkupButtonMessagePhoto, value);
            }
        }

        private bool _atachMarkupButtonMessageDocument;
        public bool AtachMarkupButtonMessageDocument
        {
            get => _atachMarkupButtonMessageDocument;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachMarkupButtonMessage.Document = value;
                Set(ref _atachMarkupButtonMessageDocument, value);
            }
        }

        private bool _atachInlineButtonMessageDefault;
        public bool AtachInlineButtonMessageDefault
        {
            get => _atachInlineButtonMessageDefault;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachInlineButtonMessage.Default = value;
                Set(ref _atachInlineButtonMessageDefault, value);
            }
        }

        private bool _atachInlineButtonMessageText;
        public bool AtachInlineButtonMessageText
        {
            get => _atachInlineButtonMessageText;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachInlineButtonMessage.Text = value;
                Set(ref _atachInlineButtonMessageText, value);
            }
        }

        private bool _atachInlineButtonMessagePhoto;
        public bool AtachInlineButtonMessagePhoto
        {
            get => _atachInlineButtonMessagePhoto;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachInlineButtonMessage.Photo = value;
                Set(ref _atachInlineButtonMessagePhoto, value);
            }
        }

        private bool _atachInlineButtonMessageDocument;
        public bool AtachInlineButtonMessageDocument
        {
            get => _atachInlineButtonMessageDocument;
            set
            {
                if (SelectedCommand == null)
                    return;
                SelectedCommand.AtachMarkupButtonMessage.Document = value;
                Set(ref _atachMarkupButtonMessageDocument, value);
            }
        }

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

        private ObservableCollection<InlineButtonProperty> _inlineButtonCollectionView;
        public ObservableCollection<InlineButtonProperty> InlineButtonCollectionView
        {
            get => _inlineButtonCollectionView;
            set => Set(ref _inlineButtonCollectionView, value);
        }

        private ObservableCollection<MarkupButtonProperty> _markupButtonCollectionView;
        public ObservableCollection<MarkupButtonProperty> MarkupButtonCollectionView
        {
            get => _markupButtonCollectionView;
            set => Set(ref _markupButtonCollectionView, value);
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
                ResetGraph();
                ResetVisibleProperty(value);
                ResetValueButtonProperty(value);
                Set(ref _selectedCommand, value);

                if (value.Documents.Count == 0)
                    value.Documents.Add(new Document());
                if (value.Photos.Count == 0)
                    value.Photos.Add(new Photo());

                PathDocument = value.Documents[0].Path;
                CaptionDocument = value.Documents[0].Caption;
                PathPhoto = value.Photos[0].Path;
                CaptionPhoto = value.Photos[0].Caption;
                AtachInlineButtonMessageDocument = value.AtachInlineButtonMessage.Document;
                AtachInlineButtonMessagePhoto = value.AtachInlineButtonMessage.Photo;
                AtachInlineButtonMessageText = value.AtachInlineButtonMessage.Text;
                AtachMarkupButtonMessageDocument = value.AtachMarkupButtonMessage.Document;
                AtachMarkupButtonMessagePhoto = value.AtachMarkupButtonMessage.Photo;
                AtachMarkupButtonMessageText = value.AtachMarkupButtonMessage.Text;
                AtachInlineButtonMessageDefault = value.AtachInlineButtonMessage.Default;
                AtachMarkupButtonMessageDefault = value.AtachMarkupButtonMessage.Default;

            }
        }

        private void ResetValueButtonProperty(IPropertyBot? buttonBot)
        {
            if (buttonBot != null)
            {
                MarkupButtonCollectionView =
                    new ObservableCollection<MarkupButtonProperty>(buttonBot.Children.OfType<MarkupButtonProperty>());
                InlineButtonCollectionView =
                    new ObservableCollection<InlineButtonProperty>(buttonBot.Children.OfType<InlineButtonProperty>());
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
            var tmp = new ObservableCollection<MarkupButtonProperty>(SelectedCommand.Children.OfType<MarkupButtonProperty>());
            MarkupButtonCollectionView = tmp;
            InlineButtonCollectionView = new ObservableCollection<InlineButtonProperty>(SelectedCommand.Children.OfType<InlineButtonProperty>());
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
            for (int i = 0; i < BotCommands.Count; i++)
            {
                if (IsNameCommandAndButtonUnique("newCommand" + i))
                {
                    BotCommands.Add(new BotCommandProperty() { Id = 0, Description = "", Name = "newCommand" + i });
                    SetStatusStartTimer("Команда добавлена");
                    break;
                }
            }
        }

        public ICommand? AddTextCommand { get; private set; }

        private void OnAddTextCommandExecuted(object p)
        {
            for (int i = 0; i < BotCommands.Count; i++)
            {
                if (IsNameCommandAndButtonUnique("newText" + i))
                {
                    BotCommands.Add(new BotTextProperty() { Name = "newText" + i });
                    SetStatusStartTimer("Команда добавлена");
                    break;
                }
            }
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
                SelectedCommand = SelectedCommand; //обновление данных
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
                if (IsNameCommandAndButtonUnique(NameMarkupButton))
                { MessageBox.Show("Название Markup кнопки должно быть уникальным"); return;}

                var markupButton = new MarkupButtonProperty() { Name = NameMarkupButton };
                SelectedCommand.Children.Add(markupButton);
                SelectedCommand = SelectedCommand;//Обновление данных
                Logger.Info($"Markup Кнопка {markupButton.Name} ID {markupButton.UniqueId} добавлена ");
                SetStatusStartTimer("Markup кнопка добавлена");

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

        private bool IsNameCommandAndButtonUnique(string name)
        {
            foreach (var item in BotCommands)
            {
                if (!IsNameUnique(item, name))
                    return false;
            }

            return true;
        }

        private bool IsNameUnique(IPropertyBot item, string name)
        {

            if (item == null)
                return true;

            if (item is MarkupButtonProperty)
            {
                if (item.Name == name)
                    return false;
            }

            foreach (var child in item.Children)
            {
                if (child is MarkupButtonProperty)
                {
                    if (!IsNameUnique(child, name))
                        return false;
                }
            }

            return true;
        }

        public ICommand? DeleteCommand { get; private set; }

        private bool CanDeleteCommandExecute(object p)
        {
            var resultSearch = false;
            if (p is not IPropertyBot bot) return resultSearch;

            if (p is BotCommandProperty || p is BotTextProperty)
            {
                resultSearch = BotCommands.Contains(bot);
            }

            else if (bot is not InlineButtonProperty && bot is not MarkupButtonProperty) return resultSearch;

            IPropertyBot parent = null;

            foreach (var item in BotCommands)
            {
                if (parent != null)
                    break;
                if (item.Children.Contains(bot))
                {
                    parent = bot;
                    break;
                }

                parent = FindParent(bot, item.Children);
            }

            if (parent != null)
                resultSearch = true;

            return resultSearch;
        }

        private void OnDeleteCommandExecuted(object p)
        {
            if (p is not IPropertyBot cmd) return;

            RemoveItemFromTree(cmd);
            SelectedCommand = null;
            SetStatusStartTimer("Элемент " + cmd.Name + " удален");
        }

        public void RemoveItemFromTree(IPropertyBot itemSearch)
        {
            IPropertyBot parent = null;

            foreach (var item in BotCommands)
            {
                if (parent != null)
                    break;
                if (item.Children.Contains(itemSearch))
                {
                    parent = item;
                    break;
                }
                parent = FindParent(itemSearch, item.Children);
            }

            parent.Children.Remove((ButtonBotBase)itemSearch);
        }

        private IPropertyBot FindParent(IPropertyBot item, ObservableCollection<ButtonBotBase> tree)
        {
            // Найти родительский элемент
            foreach (var parent in tree)
            {
                if (parent.Children.Contains(item))
                {
                    return parent;
                }
                else if (parent.Children.Count > 0)
                {
                    var result = FindParent(item, parent.Children);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public ICommand? ChooseFileBotCommand { get; private set; }

        private void OnChooseFileBotCommandExecuted(object p)
        {
            try
            {

                var ofd = new OpenFileDialog();

                if (ofd.ShowDialog() != true) return;
                if (BotCommand != null)
                    PathDocument = ofd.FileName;
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
                        PathPhoto = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public ICommand? AddDocumentsCommand { get; private set; }

        private void OnAddDocumentsCommandExecuted(object p)
        {
            try
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите комманду или кнопку");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedCommand.Documents[0].Path) ||
                    string.IsNullOrEmpty(SelectedCommand.Documents[0].Caption))
                {
                    MessageBox.Show("Обязательно добавьте путь и описание к первому файлу, далее вы сможете повоторить попытку");
                    return;
                }
                var acM = new AddContentMessage();
                var viewModel = new AddContentMessageViewModel(SelectedCommand.Documents);
                acM.DataContext = viewModel;

                if (acM.ShowDialog() == true)
                {
                    SelectedCommand.Documents = new ObservableCollection<Document>(viewModel.ContentMessageCollection.OfType<Document>());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public ICommand? AddPhotosCommand { get; private set; }

        private void OnAddPhotosCommandExecuted(object p)
        {
            try
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите комманду или кнопку");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedCommand.Photos[0].Path) ||
                    string.IsNullOrEmpty(SelectedCommand.Photos[0].Caption))
                {
                    MessageBox.Show("Обязательно добавьте путь и описание к первому изобрадению, далее вы сможете повоторить попытку");
                    return;
                }
                var acM = new AddContentMessage();
                var viewModel = new AddContentMessageViewModel(SelectedCommand.Photos);
                acM.DataContext = viewModel;

                if (acM.ShowDialog() == true)
                {
                    SelectedCommand.Photos = new ObservableCollection<Photo>(viewModel.ContentMessageCollection.OfType<Photo>());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        
        public ICommand? ChoseAttachButtonCommand { get; private set; }

        private void OnChoseAttachButtonCommandExecuted(object p)
        {
            try
            {
                if (SelectedCommand == null)
                {
                    MessageBox.Show("Выберите команду или кнопку");
                    return;
                }

                Random random = new Random();
                bool value = random.Next(2) == 0;

                if (AtachInlineButtonMessageDocument == true &&
                    AtachMarkupButtonMessageDocument == true)
                {
                    MessageBox.Show("Выберите разные типы сообщения");
                    if(value)
                        AtachInlineButtonMessageDocument = false;
                    else
                        AtachMarkupButtonMessageDocument = false;
                }
                if (AtachInlineButtonMessageText == true &&
                    AtachMarkupButtonMessageText == true)
                {
                    MessageBox.Show("Выберите разные типы сообщения");
                    if (value)
                        AtachInlineButtonMessageText = false;
                    else
                        AtachMarkupButtonMessageText = false;
                }
                if (AtachInlineButtonMessagePhoto == true &&
                    AtachMarkupButtonMessagePhoto == true)
                {
                    MessageBox.Show("Выберите разные типы сообщения");
                    if (value)
                        AtachInlineButtonMessagePhoto = false;
                    else
                        AtachMarkupButtonMessagePhoto = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
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
            AddDocumentsCommand = new LambdaCommand(OnAddDocumentsCommandExecuted, CanAlwaysFullCommandExecute);
            AddPhotosCommand = new LambdaCommand(OnAddPhotosCommandExecuted, CanAlwaysFullCommandExecute);
            // Для добавления кнопок можно изменить метод доступа так чтобы если в коллекции 6 элементов нельзя добавлять элементы
            AddInlineButtonCommand = new LambdaCommand(OnAddInlineButtonCommandExecuted, CanAlwaysFullCommandExecute);
            AddMarkupButtonCommand = new LambdaCommand(OnAddMarkupButtonCommandExecuted, CanAlwaysFullCommandExecute);
            ChoseAttachButtonCommand = new LambdaCommand(OnChoseAttachButtonCommandExecuted, CanAlwaysFullCommandExecute);

            Test = new LambdaCommand(OnTestExecuted, CanAlwaysFullCommandExecute);
        }



        public ProjectWindowViewModel()
        {
            #region Inicialization
            //VisibilityBotCommand = Visibility.Hidden;
            //VisibilityBotText = Visibility.Hidden;
            BotCommands = new ObservableCollection<IPropertyBot>() { new BotCommandProperty() { Id = 0, Description = "Начальная команда", Name = "start", Children = new ObservableCollection<ButtonBotBase>() { new InlineButtonProperty() { Name = "Inline Button" }, new MarkupButtonProperty() { Name = "Markup Button" } } } };
            InlineButtonCollectionView = new ObservableCollection<InlineButtonProperty>() { new InlineButtonProperty() { Name = "TEST" } };
            MarkupButtonCollectionView = new ObservableCollection<MarkupButtonProperty>();
            #endregion

            #region Command

            InitializationCommand();

            #endregion
            //SaveProjectCommand.Execute(null);
            ResetGraph();

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 5);
        }

        private BidirectionalGraph<object, IEdge<object>> _graph;
        public BidirectionalGraph<object, IEdge<object>> Graph
        {
            get => _graph;
            set => Set(ref _graph, value);
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
            InlineButtonCollectionView = new ObservableCollection<InlineButtonProperty>();
            MarkupButtonCollectionView = new ObservableCollection<MarkupButtonProperty>();
            #endregion

            #region Command

            InitializationCommand();

            #endregion

            ResetGraph();


            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 5);
        }

        private void ResetGraph()
        {
            var graph = new BidirectionalGraph<object, IEdge<object>>();
            var retru = new Retru() { Children = BotCommands };
            AddTreeNode(retru, graph);
            Graph = graph;
        }
        private void AddTreeNode(Retru node, BidirectionalGraph<object, IEdge<object>> graph)
        {
            //graph.AddVertex(node.Name);

            foreach (var childNode in node.Children)
            {
                // graph.AddVertex(childNode.Name);
                //graph.AddEdge(new Edge<object>(node.Name, childNode.Name));
                AddTreeNodeToGraph(childNode, graph);
            }
        }

        private void AddTreeNodeToGraph(IPropertyBot node, BidirectionalGraph<object, IEdge<object>> graph)
        {
            if (node is InlineButtonProperty || node is MarkupButtonProperty)
            {
                var button = (ButtonBotBase)node;
                graph.AddVertex(node.Name + " by " + button.UniqueId.Substring(0, 5));

                foreach (var childNode in node.Children)
                {
                    graph.AddVertex(childNode.Name + " by " + childNode.UniqueId.Substring(0, 5));
                    graph.AddEdge(new Edge<object>(node.Name + " by " + button.UniqueId.Substring(0, 5), childNode.Name + " by " + childNode.UniqueId.Substring(0, 5)));
                    AddTreeNodeToGraph(childNode, graph);
                }
            }
            else
            {
                graph.AddVertex(node.Name);

                foreach (var childNode in node.Children)
                {
                    graph.AddVertex(childNode.Name + " by " + childNode.UniqueId.Substring(0, 5));
                    graph.AddEdge(new Edge<object>(node.Name, childNode.Name + " by " + childNode.UniqueId.Substring(0, 5)));
                    AddTreeNodeToGraph(childNode, graph);
                }
            }
        }

        public class Retru
        {
            public string Name { get; set; }
            public ObservableCollection<IPropertyBot> Children { get; set; }
        }
    }
}
