using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.ViewData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChatbotConstructorTelegram.Model.File
{
    [Serializable]
    public class WrapperDataBot
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public string? Token { get; set; } 
        public string? PathDirectory { get; set; } 
        public string? Path { get; set; }
        public bool IsReadyAiogram { get; set; } 
        public bool IsReadyPython { get; set; } 
        public List<BotCommandProperty>? CommandProperties { get; set; }
        public List<BotTextProperty>? TextProperties { get; set; } 

        public WrapperDataBot(ObservableCollection<IPropertyBot> properties)
        {
            Name = DataProject.Name;
            Description = DataProject.Description;
            Token = DataProject.Token;
            PathDirectory = DataProject.PathDirectory;
            Path = DataProject.Path;
            IsReadyAiogram = DataProject.IsReadyAiogram;
            IsReadyPython = DataProject.IsReadyPython;
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
