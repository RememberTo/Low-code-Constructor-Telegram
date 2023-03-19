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
            var viewModel = new ProjectWindowViewModel();
            DataContext = viewModel;
        }

        public ProjectWindow(string path)
        {
            InitializeComponent();
            var viewModel = new ProjectWindowViewModel(path);
            DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((ProjectWindowViewModel)this.DataContext).CloseApplicationCommand?.Execute(null);
            e.Cancel = ((ProjectWindowViewModel)this.DataContext).IsCancel;
        }
    }
}
