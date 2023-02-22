using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public class MarkupButtonProperty : IPropertyBot, IButtonBot
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public string UniqueId { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<InlineButtonProperty> InlineButtons { get; set; }
        public ObservableCollection<MarkupButtonProperty> MarkupButtons { get; set; }
        public ObservableCollection<IPropertyBot> Buttons { get; set; }
        public MarkupButtonProperty()
        {
            InlineButtons = new ObservableCollection<InlineButtonProperty>();
            MarkupButtons = new ObservableCollection<MarkupButtonProperty>();
            Buttons = new ObservableCollection<IPropertyBot>();
            UniqueId = Guid.NewGuid().ToString();

            InlineButtons.CollectionChanged += InlineButtons_CollectionChanged;
            MarkupButtons.CollectionChanged += MarkupButtons_CollectionChanged;
        }

        private void MarkupButtons_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var collection = (ObservableCollection<MarkupButtonProperty>)sender;
            var b = (IPropertyBot)collection.LastOrDefault();
            Buttons.Add(b);
        }

        private void InlineButtons_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var collection = (ObservableCollection<InlineButtonProperty>)sender;
            var b = (IPropertyBot)collection.LastOrDefault();
            Buttons.Add(b);
        }

        public void Dispose()
        {
            InlineButtons.CollectionChanged -= InlineButtons_CollectionChanged;
            MarkupButtons.CollectionChanged -= MarkupButtons_CollectionChanged;
        }
    }
}
