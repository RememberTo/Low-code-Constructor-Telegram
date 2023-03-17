using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView
{
    public interface IPropertyBot : IPropetyContainer
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public int CountButtonInLine { get; set; }
        public string URL { get; set; }
        public string? Description { get; set; }
        public TypeMessage AtachInlineButtonMessage { get; set; }
        public TypeMessage AtachMarkupButtonMessage { get; set; }
        public ObservableCollection<Photo> Photos { get; set; }
        public ObservableCollection<Document> Documents { get; set; }
    }
}
