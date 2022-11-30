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

namespace ChatbotConstructorTelegram.View.Window
{
    /// <summary>
    /// Логика взаимодействия для BotCreationWindow.xaml
    /// </summary>
    public partial class BotCreationWindow : System.Windows.Window
    {
        public string Path { get; private set; }
        public BotCreationWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
