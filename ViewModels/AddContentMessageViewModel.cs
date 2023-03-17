using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatbotConstructorTelegram.Infrastructure.Commands;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.ViewModels.Base;
using Microsoft.Win32;
using NLog;

namespace ChatbotConstructorTelegram.ViewModels
{
    internal class AddContentMessageViewModel : ViewModel
    {
        private bool _isDocuments;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private ObservableCollection<IPropertyFile> _contCollection;
        public ObservableCollection<IPropertyFile> ContentMessageCollection
        {
            get => _contCollection;
            set => Set(ref _contCollection, value);
        }

        private IPropertyFile _selectedContent;

        public IPropertyFile SelectedContent
        {
            get => _selectedContent;
            set => Set(ref _selectedContent, value);
        }

        #region Command
        private bool CanAlwaysFullCommandExecute(object p)
        {
            return true;
        }

        public ICommand? AddContentCommand { get; private set; }
        private void OnAddContentCommandExecuted(object p)
        {
            try
            {
                ContentMessageCollection.Add((_isDocuments) ? new Document(){Path = "C://"} : new Photo(){Path = "C://"});
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public ICommand? ChoseFileCommand { get; private set; }
        private void OnChoseFileCommandExecuted(object p)
        {
            try
            {
                var ofd = new OpenFileDialog();

                if (!_isDocuments)
                    ofd.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

                if (ofd.ShowDialog() == true)
                {
                    if (SelectedContent != null)
                        SelectedContent.Path = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public ICommand? DeleteCommand { get; private set; }
        private void OnDeleteCommandExecuted(object p)
        {
            try
            {
                ContentMessageCollection.Remove((IPropertyFile)SelectedContent);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private bool CanDeleteCommandExecute(object p)
        {
            return SelectedContent is Document or Photo;
        }

        #endregion

        private void InitializationCommand()
        {
            AddContentCommand = new LambdaCommand(OnAddContentCommandExecuted, CanAlwaysFullCommandExecute);
            ChoseFileCommand = new LambdaCommand(OnChoseFileCommandExecuted, CanAlwaysFullCommandExecute);
            DeleteCommand = new LambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
        }

        public AddContentMessageViewModel(ObservableCollection<Document> documents)
        {
            InitializationCommand();

            _isDocuments = true;
            ContentMessageCollection = new ObservableCollection<IPropertyFile>();

            foreach (var item in documents)
            {
                ContentMessageCollection.Add(item);
            }
        }
        public AddContentMessageViewModel(ObservableCollection<Photo> photos)
        {
            InitializationCommand();

            _isDocuments = false;
            ContentMessageCollection = new ObservableCollection<IPropertyFile>();

            foreach (var item in photos)
            {
                ContentMessageCollection.Add(item);
            }
        }
    }
}
