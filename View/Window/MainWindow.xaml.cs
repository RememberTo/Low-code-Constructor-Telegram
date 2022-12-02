using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ChatbotConstructorTelegram.Model.ViewData;
using NLog;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var createBotWnd = new CreationProjectWindow();
            createBotWnd.Show();
            Log.Info("Создание проекта");
            this.Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var path = ((RecentProject)((System.Windows.Controls.Button)sender).DataContext).Path;
                Process.Start(path);
                Log.Info($"Проект {path} запущен");
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                MessageBox.Show(exception.Message);

            }
        }
    }
}
