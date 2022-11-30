using System;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Threading;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Логика взаимодействия для BotCreationWindow.xaml
    /// </summary>
    public partial class BotCreationWindow : System.Windows.Window
    {
        public string PathDirectory { get; private set; }
        public string Token { get; private set; }
        public string NameProject { get; private set; }
        private DispatcherTimer _timer;
        public BotCreationWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 5);
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            Token = TextBox_Token.Text;
            NameProject = TextBox_NameProject.Text;
            PathDirectory = TextBox_Path.Text;

            if (Directory.Exists(PathDirectory) && Token != string.Empty && NameProject != string.Empty)
                DialogResult = true;
            else
            {
                TextBlock_Warning.Visibility = Visibility.Visible;
                _timer.Start();
                if (!Directory.Exists(PathDirectory))
                    TextBlock_Warning.Text = "Введенной директории не существует";
                else
                    TextBlock_Warning.Text = "Заполните все поля";
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonChoseDirectory_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog ofd = new() { IsFolderPicker = true };
            if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                TextBox_Path.Text = ofd.FileName;
            }
            this.Focus();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TextBlock_Warning.Visibility = Visibility.Hidden;
        }
    }
}
