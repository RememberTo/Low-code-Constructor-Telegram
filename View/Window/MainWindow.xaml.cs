using NLog;
using System.Windows.Input;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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

    }
}
