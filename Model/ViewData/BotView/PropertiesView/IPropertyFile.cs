using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView
{
    internal interface IPropertyFile
    {
        public string Path { get; set; }
        public string Caption { get; set; }
    }
}
