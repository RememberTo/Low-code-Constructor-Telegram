using ChatbotConstructorTelegram.Infrastructure.Manager;
using ChatbotConstructorTelegram.Model.Bot;
using ChatbotConstructorTelegram.Model.StaticData;
using ChatbotConstructorTelegram.Model.ViewData;
using ChatbotConstructorTelegram.Resources;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ChatbotConstructorTelegram.Infrastructure.Commands.Base;
using ChatbotConstructorTelegram.Infrastructure.Python;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;

namespace ChatbotConstructorTelegram.Infrastructure
{
    internal class BotCodeGenerator
    {
        private readonly Logger _logger  = LogManager.GetCurrentClassLogger();
        private readonly List<BotCommandProperty> _botCommandList = new();
        private readonly List<BotTextProperty> _botTextList = new();
        private readonly StringBuilder _sbCode = new();

        public BotCodeGenerator(ObservableCollection<IPropertyBot> botCommands)
        {
            SplitCommand(botCommands);
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

        private void SplitCommand(ObservableCollection<IPropertyBot> botCommands)
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
                    result = "print('InlineButton "+inlineButton.UniqueId+" | "+inlineButton.Name+"')\n\n";
                }

                else if (button is MarkupButtonProperty markupButton)
                {
                    result = "print('MarkupButton " + markupButton.UniqueId+" | "+markupButton.Name + "')\n\n";
                }

                _sbCode.Append(result);
                // Генерация кода для Inline кнопки
                // ...
                // Генерация кода для Markup кнопки
                // ...
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
