using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public class InlineButtonProperty : IPropertyBot
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<InlineButtonProperty> Buttons { get; set; }

        public InlineButtonProperty()
        {
            Buttons = new ObservableCollection<InlineButtonProperty>();
        }
    }
}
