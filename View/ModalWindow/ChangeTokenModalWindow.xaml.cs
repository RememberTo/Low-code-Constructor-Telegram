using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChatbotConstructorTelegram.Model.Bot;

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
                StaticDataBot.Token = TextBox_Token.Text;
                DialogResult = true;
            }
            else 
                DialogResult = false;
        }
    }
}
