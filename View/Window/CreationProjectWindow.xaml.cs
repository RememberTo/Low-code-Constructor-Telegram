using System;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Threading;
using ChatbotConstructorTelegram.Model.Bot;
using NLog;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Логика взаимодействия для BotCreationWindow.xaml
    /// </summary>
    public partial class CreationProjectWindow : System.Windows.Window
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public CreationProjectWindow()
        {
            InitializeComponent();
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(TextBox_Path.Text) &&
                  TextBox_Token.Text != string.Empty &&
                  TextBox_NameProject.Text != string.Empty)
            {
                var projWnd = new ProjectWindow();
                Log.Info($"Проверка пройдена запущен проект {TextBox_NameProject.Text} путь {TextBox_Path.Text}");
                projWnd.Show();
                this.Close();
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWnd = new MainWindow();
            mainWnd.Show();
            this.Close();
        }
    }
}
