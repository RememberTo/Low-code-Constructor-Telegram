using NLog;
using System.IO;
using System.Windows;
using ChatbotConstructorTelegram.ViewModels;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Логика взаимодействия для BotCreationWindow.xaml
    /// </summary>
    public partial class CreationProjectWindow : System.Windows.Window
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public CreationProjectWindow()
        {
            InitializeComponent();
            var viewModel = new CreationProjectViewModel();
            viewModel.RequestClose += (s, e) => Close();
            DataContext = viewModel;
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(TextBox_Path.Text) &&
                  TextBox_Token.Text != string.Empty &&
                  TextBox_NameProject.Text != string.Empty)
            {
                Log.Info($"Проверка пройдена запущен проект {TextBox_NameProject.Text} путь {TextBox_Path.Text}");
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
