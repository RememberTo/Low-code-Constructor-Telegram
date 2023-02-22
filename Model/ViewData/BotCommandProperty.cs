using System;
using ChatbotConstructorTelegram.Model.ViewData.PropertiesView;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatbotConstructorTelegram.Model.ViewData
{
    public class BotCommandProperty : IPropertyBot, IDisposable
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public Photo Photo { get; set; }
        public Document Document { get; set; }
        public ObservableCollection<IPropertyBot> Buttons { get; set; }
        public ObservableCollection<InlineButtonProperty> InlineButtons { get; set; }
        public ObservableCollection<MarkupButtonProperty> MarkupButtons { get; set; }

        public BotCommandProperty()
        {
            Buttons = new ObservableCollection<IPropertyBot>();
            InlineButtons = new ObservableCollection<InlineButtonProperty>();
            MarkupButtons = new ObservableCollection<MarkupButtonProperty>();
            Document = new Document();
            Photo = new Photo();

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
