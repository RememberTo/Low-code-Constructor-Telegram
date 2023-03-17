using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.ViewModels;

namespace ChatbotConstructorTelegram.View.ModalWindow
{
    /// <summary>
    /// Логика взаимодействия для AddContentMessage.xaml
    /// </summary>
    public partial class AddContentMessage : System.Windows.Window
    {
        public AddContentMessage()
        {
            InitializeComponent();
            
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_Break(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
