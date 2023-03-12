using System;
using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.Button
{
    public class MarkupButtonProperty : ButtonBotBase
    {
        public MarkupButtonProperty()
        {
            Children = new ObservableCollection<ButtonBotBase>();
            UniqueId = Guid.NewGuid().ToString();
        }
    }
}
