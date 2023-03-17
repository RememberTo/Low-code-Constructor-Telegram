using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView
{
    public class TypeMessage
    {
        public bool Text { get; set; }
        public bool Photo { get; set; }
        public bool Document { get; set; }
        public bool Default { get; set; }

        public string GetTrueTypeMessage()
        {
            if (Text)
                return "Text";
            if (Photo)
                return "Photo";
            if (Document)
                return "Document";

            return "Default";
        }

        public string GetFalseTypeMessage()
        {
            if (Text)
                return "Text";
            if (Photo)
                return "Photo";
            if (Document)
                return "Document";

            return "Default";
        }
    }
}
