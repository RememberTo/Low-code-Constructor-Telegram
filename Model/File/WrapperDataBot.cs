using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.ViewData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Model.File
{
    [Serializable]
    public class WrapperDataBot
    {
        public DataProject DataProject { get; set; }
        public List<BotCommandProperty>? CommandProperties { get; set; }
        public List<BotTextProperty>? TextProperties { get; set; } 

        public WrapperDataBot(ObservableCollection<IPropertyBot> properties)
        {
            DataProject = DataProject.Instance;
            TextProperties = new List<BotTextProperty>();
            CommandProperties = new List<BotCommandProperty>();

            foreach (var botCommand in properties)
            {
                if (botCommand is BotCommandProperty)
                    CommandProperties.Add((BotCommandProperty)botCommand);
                if (botCommand is BotTextProperty)
                    TextProperties.Add((BotTextProperty)botCommand);
            }
        }
        public WrapperDataBot()
        {
            
        }
    }
}
