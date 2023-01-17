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
using ChatbotConstructorTelegram.Infrastructure.Python;

namespace ChatbotConstructorTelegram.Infrastructure
{
    internal class Bot
    {
        private readonly Logger _logger  = LogManager.GetCurrentClassLogger();
        private readonly List<BotCommandProperty> _botCommandList = new();
        private readonly List<BotTextProperty> _botTextList = new();
        private readonly StringBuilder _sbCode = new();

        public Bot(ObservableCollection<IPropertyBot> botCommands, ParametersBot parametersBot)
        {
            SplitCommand(botCommands);
        }

        public void CreateBot()
        {
            try
            {
                CreateIncludeData();
                GenerateFunctionAsync();

                var path = ExplorerManager.LocationProject;

                path += "\\Test\\test.py";

                _sbCode.AppendLine(ResourceGlob.StartPoll);
                PythonHelper.WriteCodeFileAsync(path, _sbCode.ToString(), FileMode.Create);

                TerminalManager.ExecuteConsoleCommand(TerminalCommands.TestStart);
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
            _sbCode.AppendLine(ResourceGlob.ImportAiogram);
            _sbCode.AppendLine(ResourceGlob.CreateBot.Replace("TOKEN", @"'5154449316:AAEE_JeL9Aha81J-jn4anTOkziuCgdM3Q5w'"/*DataProject.Token*/));
            _sbCode.AppendLine("\nprint('Bot start')\n\n");
        }

        private void GenerateFunctionAsync()
        {
            GenerateCommandAsync();
            GenerateTextAsync();
        }

        private void GenerateTextAsync()
        {
        }

        private void GenerateCommandAsync()
        {
            foreach (var botCommand in _botCommandList)
            {
                var decorator = new Decorator(ResourceFunc.Message, ResourceFunc.ParamCommand, botCommand.Name);
                var funcPy = new FunctionPy(decorator, botCommand.Name + "_handler", true, "message");
                _sbCode.Append(funcPy.GeneratedFunction() + "\n");
                var body = funcPy.GeneratedBody(botCommand);
                _sbCode.Append(body + "\n");
            }
        }

       
    }
}
