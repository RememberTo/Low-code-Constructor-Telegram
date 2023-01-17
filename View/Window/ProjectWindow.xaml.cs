using ChatbotConstructorTelegram.ViewModels;
using NLog;
using System.Windows;

namespace ChatbotConstructorTelegram.View.Window
{
    public partial class ProjectWindow : System.Windows.Window
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public ProjectWindow()
        {
            InitializeComponent();
            //this.DataContext = new ProjectWindowViewModel();
        }

        public ProjectWindow(string path)
        {
            InitializeComponent();
            this.DataContext = new ProjectWindowViewModel(path);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
