using System.Text;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Resources;
using NLog;

namespace ChatbotConstructorTelegram.Infrastructure.Python
{
    internal class FunctionPy
    {
        public Decorator Decorator { get; set; }

        public string Name { get; set; }
        public bool isAsync { get; set; }
        public string Parameter { get; set; }


        private string SendText = "await bot.send_message(call.message.chat.id, text='name', caption='TEXT' reply_markup=markup_inline)";
        private string SendPhoto = "await bot.send_photo(call.message.chat.id, photo='PHOTO', caption='name' reply_markup = markup_inline)";
        private string SendDocument = "await bot.send_document(call.message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_inline)";

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public FunctionPy(Decorator Decorator, string Name, bool isAsync, string Parameter)
        {
            this.Decorator = Decorator;
            this.Name = Name;
            this.isAsync = isAsync;
            this.Parameter = Parameter;
        }

        public string GeneratedFunction()
        {
            var formationFunc = new StringBuilder();
            formationFunc.Append(Decorator.NameFunc + "(" +
                                 Decorator.TypeParameters.Replace("name", Decorator.Parameter) + ")\n");
            formationFunc.Append(((isAsync) ? "async " : "") + "def " + Name + "(" + Parameter + "):");

            return formationFunc.ToString();
        }

        public string GeneratedBody(BotCommandProperty bcp)
        {
            var sb = new StringBuilder();

            //foreach (var item in bcp.Children)
            //{
            //    if (item is InlineButtonProperty inlineButton)
            //    {
            //        var inline = new InlineButton((InlineButtonProperty)bcp);
            //        sb.AppendLine(inline.GenerateButton());
            //    }
            //    else if (item is MarkupButtonProperty markupButton)
            //    {

            //    }
            //}

            //if (string.IsNullOrEmpty(bcp.Text) == false)
            //    sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("name", bcp.Text));
            //if (string.IsNullOrEmpty(bcp.Document.Path) == false)
            //    sb.AppendLine("\t" +
            //                  (ResourceFunc.BotSendDocument.Replace("PATH", bcp.Document.Path)).Replace("name",
            //                      bcp.Document.Caption));
            //if (string.IsNullOrEmpty(bcp.Photo.Path) == false)
            //    sb.AppendLine("\t" +
            //                  (ResourceFunc.BotSendPhoto.Replace("PATH", bcp.Photo.Path)).Replace("name",
            //                      bcp.Photo.Caption));

            return sb.ToString();
        }

    }
}