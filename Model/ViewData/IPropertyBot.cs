using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Windows.Input;
using ChatbotConstructorTelegram.Infrastructure.Python;
using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public interface IPropertyBot : IDisposable
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<InlineButtonProperty> InlineButtons { get; set; }
        public ObservableCollection<MarkupButtonProperty> MarkupButtons { get; set; }
        public ObservableCollection<IPropertyBot> Buttons { get; set; }

    }
}
