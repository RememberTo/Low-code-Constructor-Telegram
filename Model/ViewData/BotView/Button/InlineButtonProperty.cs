using System;
using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.Button
{
    [Serializable]
    public class InlineButtonProperty : ButtonBotBase
    {
        public InlineButtonProperty()
        {
            Documents = new ObservableCollection<Document>(){};
            Photos = new ObservableCollection<Photo>() { };
            AtachInlineButtonMessage = new TypeMessage() { Default = true };
            AtachMarkupButtonMessage = new TypeMessage() { Default = true };
            Children = new ObservableCollection<ButtonBotBase>();
            UniqueId = Guid.NewGuid().ToString();
        }
    }
}
