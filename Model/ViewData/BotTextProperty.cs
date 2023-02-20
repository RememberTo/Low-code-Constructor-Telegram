using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;
using System.Collections.ObjectModel;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public class BotTextProperty : IPropertyBot
    {
        public string? Name { get; set; }
        public string Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<InlineButtonProperty> Buttons { get; set; }

        public BotTextProperty()
        {
            Document = new Document();
            Photo = new Photo();
            Buttons = new ObservableCollection<InlineButtonProperty>();
        }
    }
}
