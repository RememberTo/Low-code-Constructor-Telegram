using System.Text;
using ChatbotConstructorTelegram.Model.ViewData;
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

            if (string.IsNullOrEmpty(bcp.Text) == false)
                sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("name", bcp.Text));
            if (string.IsNullOrEmpty(bcp.Document.Path) == false)
                sb.AppendLine("\t" +
                              (ResourceFunc.BotSendDocument.Replace("PATH", bcp.Document.Path)).Replace("name",
                                  bcp.Document.Caption));
            if (string.IsNullOrEmpty(bcp.Photo.Path) == false)
                sb.AppendLine("\t" +
                              (ResourceFunc.BotSendPhoto.Replace("PATH", bcp.Photo.Path)).Replace("name",
                                  bcp.Photo.Caption));

            return sb.ToString();
        }

    }
}