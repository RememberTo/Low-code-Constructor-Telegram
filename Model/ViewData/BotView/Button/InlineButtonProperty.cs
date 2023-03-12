using System;
using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.Button
{
    public class InlineButtonProperty : ButtonBotBase
    {
        public InlineButtonProperty()
        {
            Children = new ObservableCollection<ButtonBotBase>();
            UniqueId = Guid.NewGuid().ToString();
        }
    }
}
