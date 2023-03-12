using System.Collections.ObjectModel;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView
{
    public interface IPropetyContainer
    {
        public ObservableCollection<ButtonBotBase> Children { get; set; }
    }
}