using System.Collections.ObjectModel;
using System.Xml.Serialization;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.Command
{
    [XmlInclude(typeof(InlineButtonProperty))]
    [XmlInclude(typeof(MarkupButtonProperty))]
    public class BotCommandProperty : IPropertyBot
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<ButtonBotBase> Children { get; set; }

        public BotCommandProperty()
        {
            Children = new ObservableCollection<ButtonBotBase>();
            Document = new Document();
            Photo = new Photo();
        }
    }
}
