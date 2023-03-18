using ChatbotConstructorTelegram.Infrastructure.Python;
using ChatbotConstructorTelegram.Infrastructure.Python.Formation;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;
using ChatbotConstructorTelegram.Resources;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Infrastructure
{
    internal class BotCodeGenerator
    {
        private readonly Logger _logger  = LogManager.GetCurrentClassLogger();
        private readonly List<BotCommandProperty> _botCommandList = new();
        private readonly List<BotTextProperty> _botTextList = new();
        private List<IPropertyBot> BotCommands;
        private readonly StringBuilder _sbCode = new();

        public BotCodeGenerator(ObservableCollection<IPropertyBot> botCommands)
        {
            BotCommands = DeepCopy(botCommands.ToList());
            
            SplitCommand(BotCommands);

            foreach (var item in _botCommandList)
            {
                ReplaceEscapedChar(item);
            }
            foreach (var item in _botTextList)
            {
                ReplaceEscapedChar(item);
            }

        }

        public static List<T> DeepCopy<T>(List<T> list)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, list);
                ms.Position = 0;
                return (List<T>)formatter.Deserialize(ms);
            }
        }

        public void CreateBot()
        {
            try
            {
                CreateIncludeData();
                GenerateFunctionAsync();
                CreateExitAndPooling();

                var pathPythonFile = DataProject.Instance.PathDirectory + "\\" + DataProject.Instance.Name + ".py";

                PythonHelper.WriteCodeFileAsync(pathPythonFile, _sbCode.ToString(), FileMode.Create);
                DataProject.Instance.PathLastPythonFile = pathPythonFile;
                //TerminalManager.ExecuteConsoleCommand(TerminalCommands.TestStart);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void ReplaceEscapedChar(IPropertyBot buttonProperty)
        {
            if (buttonProperty == null)
                return;

            foreach (var item in buttonProperty.Documents)
            {
                item.Path = item.Path.Replace("\\", "\\\\");
            }

            foreach (var item in buttonProperty.Photos)
            {
                item.Path = item.Path.Replace("\\", "\\\\");
            }

            foreach (var child in buttonProperty.Children)
            {
                ReplaceEscapedChar(child);
            }
        }

        private void SplitCommand(List<IPropertyBot> botCommands)
        {
            foreach (var botCommand in botCommands)
            {
                if (botCommand is BotCommandProperty)
                    _botCommandList.Add((BotCommandProperty)botCommand);
                if(botCommand is BotTextProperty)
                    _botTextList.Add((BotTextProperty)botCommand);
            }
        }

        private void CreateIncludeData()
        {
            _sbCode.AppendLine(ResourceGlob.ImportLibrary);
            _sbCode.AppendLine(ResourceGlob.CreateBot.Replace("TOKEN", @"'5154449316:AAEE_JeL9Aha81J-jn4anTOkziuCgdM3Q5w'"/*DataProject.Token*/));
            _sbCode.AppendLine("\nprint('Bot start')\n\n");
        }

        private void CreateExitAndPooling()
        {
            _sbCode.AppendLine(ResourceGlob.ExitFunc);
            _sbCode.AppendLine(ResourceGlob.StartPoll);
        }

        private void GenerateFunctionAsync()
        {
            GenerateCommandAsync();
            GenerateTextAsync();
        }

        private void GenerateTextAsync()
        {
        }

        public void GenerateButtonParallel(ObservableCollection<ButtonBotBase> buttonBotBases)
        {
            Parallel.ForEach(buttonBotBases, button =>
            {
                var result = string.Empty;

                if (button is InlineButtonProperty inlineButton)
                {
                    var inline = new InlineButton(inlineButton);
                    result = inline.GenerateFunc();
                }

                else if (button is MarkupButtonProperty markupButton)
                {
                    var markup = new MarkupButton(markupButton);
                    result = markup.GenerateFunc();
                }

                _sbCode.Append(result);
                // Рекурсивный вызов генерации кода для вложенных команд
                if (button.Children.Count > 0)
                {
                    GenerateButtonParallel(button.Children);
                }
            });
        }

        private void GenerateCommandAsync()
        {
            foreach (var botCommand in _botCommandList)
            {
                _sbCode.Append(GenerateCodeForFunction(botCommand));
                GenerateButtonParallel(botCommand.Children);
            }
        }

        private string GenerateCodeForFunction(BotCommandProperty item)
        {
            var command = new BotCommand(item);
            return command.GenerateFunc();
            //var decorator = new Decorator(ResourceFunc.Message, ResourceFunc.ParamCommand, item.Name);
            //var funcPy = new FunctionPy(decorator, item.Name + "_handler", true, "message");
            //var body = funcPy.GeneratedBody(item);

            //return funcPy.GeneratedFunction() + "\n" + body + "\n";
        }
       
    }
}
