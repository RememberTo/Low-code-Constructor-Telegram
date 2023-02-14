using ChatbotConstructorTelegram.Model.Bot;
using System.Windows;

namespace ChatbotConstructorTelegram.View.ModalWindow
{
    /// <summary>
    /// Логика взаимодействия для ChangeToken.xaml
    /// </summary>
    public partial class ChangeTokenModalWindow : System.Windows.Window
    {
        public ChangeTokenModalWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBox_Token.Text != string.Empty)
            {
                DataProject.Instance.Token = TextBox_Token.Text;
                DialogResult = true;
            }
            else 
                DialogResult = false;
        }
    }
}
