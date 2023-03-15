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
        public Photo Photo { get; set; }
        public Document Document { get; set; }
    }
}
