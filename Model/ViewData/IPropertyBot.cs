using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using ChatbotConstructorTelegram.Infrastructure.Python;
using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public interface IPropertyBot
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<InlineButtonProperty> Buttons { get; set; }
    }
}
