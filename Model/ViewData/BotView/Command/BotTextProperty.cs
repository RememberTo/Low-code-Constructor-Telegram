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
        public TypeMessage AtachInlineButtonMessage { get; set; }
        public TypeMessage AtachMarkupButtonMessage { get; set; }
        public ObservableCollection<Photo> Photos { get; set; }
        public ObservableCollection<Document> Documents { get; set; }
        public ObservableCollection<ButtonBotBase> Children { get; set; }

        public BotTextProperty()
        {
            Children = new ObservableCollection<ButtonBotBase>();
            Documents = new ObservableCollection<Document>() {  };
            Photos = new ObservableCollection<Photo>() { };
            AtachInlineButtonMessage = new TypeMessage() { Default = true };
            AtachMarkupButtonMessage = new TypeMessage() { Default = true };
        }
    }
}
