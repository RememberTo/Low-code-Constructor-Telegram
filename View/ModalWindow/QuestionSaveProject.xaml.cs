using System.Windows;

namespace ChatbotConstructorTelegram.View.ModalWindow
{
    /// <summary>
    /// Логика взаимодействия для QuestionSaveProject.xaml
    /// </summary>
    public partial class QuestionSaveProject : System.Windows.Window
    {
        public bool IsSaveAsFile { get; set; }

        public QuestionSaveProject()
        {
            InitializeComponent();
        }

        private void Button_Click_Yes(object sender, RoutedEventArgs e)
        {
            IsSaveAsFile = true;
            DialogResult = true;
        }

        private void Button_Click_No(object sender, RoutedEventArgs e)
        {
            IsSaveAsFile = false;
            DialogResult = true;
        }

        private void Button_Click_Break(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
