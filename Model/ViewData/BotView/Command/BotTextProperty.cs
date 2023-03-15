using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.Command
{
    public class BotTextProperty : IPropertyBot
    {
        public string? Name { get; set; }
        public string Text { get; set; }
        public int CountButtonInLine { get; set; }
        public string URL { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<ButtonBotBase> Children { get; set; }

        public BotTextProperty()
        {
            Children = new ObservableCollection<ButtonBotBase>();
            Document = new Document();
            Photo = new Photo();
        }
    }
}
