using System.Windows;
using ChatbotConstructorTelegram.ViewModels;

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Логика взаимодействия для StartBotWindow.xaml
    /// </summary>
    public partial class StartBotWindow : System.Windows.Window
    {
        public StartBotWindow()
        {
            InitializeComponent();
            //this.DataContext = new StartBotWindowViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((StartBotWindowViewModel)this.DataContext).IsPool)
            {
                ((StartBotWindowViewModel)this.DataContext).StopPollingCommand?.Execute(null);
            }
        }
    }
}
