using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChatbotConstructorTelegram.Infrastructure.Python.Literals;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Command;
using ChatbotConstructorTelegram.Model.ViewData.BotView.SampleView;
using ChatbotConstructorTelegram.Resources;

namespace ChatbotConstructorTelegram.Infrastructure.Python.Formation
{
    internal class BotCommand
    {
        public BotCommandProperty ButtonProperty { get; set; }
        public List<InlineButtonProperty> InlineButtons { get; set; }
        public List<MarkupButtonProperty> MarkupButtons { get; set; }
        public int CountNestedButton { get; set; }

        public BotCommand(BotCommandProperty buttonProperty)
        {
            ButtonProperty = buttonProperty ?? throw new ArgumentNullException(nameof(buttonProperty));
            CountNestedButton = buttonProperty.Children.Count;

            InlineButtons = ButtonProperty.Children.OfType<InlineButtonProperty>().ToList();
            MarkupButtons = ButtonProperty.Children.OfType<MarkupButtonProperty>().ToList();
        }

        public string GenerateFunc()
        {
            var sb = new StringBuilder();

            var decorator = new Decorator(ResourceFunc.Message, ResourceFunc.ParamCommand, ButtonProperty.Name);
            var funcPy = new FunctionPy(decorator, ButtonProperty.Name + "_handler", true, "message");

            sb.AppendLine(funcPy.GeneratedFunction());

            sb.AppendLine(GenerateButtons());
            sb.AppendLine(GenerateSendMessages());

            return sb.ToString();
        }

        private string? GenerateSendMessages()
        {
            var sb = new StringBuilder();
            var typemsg = new TypeMessage();

            switch (ButtonProperty.AtachInlineButtonMessage.GetTrueTypeMessage())
            {
                case "Text":
                    if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    {
                        sb.AppendLine(GenerateSendMessageText(false));
                        typemsg.Text = true;
                    }
                    break;
                case "Document":
                    if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    {
                        sb.AppendLine(GenerateSendMessageDocuments(false));
                        typemsg.Document = true;
                    }
                    break;
                case "Photo":
                    if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    {
                        sb.AppendLine(GenerateSendMessagePhotos(false));
                        typemsg.Photo = true;
                    }
                    break;
            }

            switch (ButtonProperty.AtachMarkupButtonMessage.GetTrueTypeMessage())
            {
                case "Text":
                    if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    {
                        sb.AppendLine(GenerateSendMessageTextMarkup(false));
                        typemsg.Text = true;
                    }
                    break;
                case "Document":
                    if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    {
                        sb.AppendLine(GenerateSendMessageDocumentsMarkup(false));
                        typemsg.Document = true;
                    }
                    break;
                case "Photo":
                    if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    {
                        sb.AppendLine(GenerateSendMessagePhotosMarkup(false));
                        typemsg.Photo = true;
                    }
                    break;
            }

            if (typemsg.Text && typemsg.Document)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    sb.AppendLine(GenerateSendMessagePhotosMarkup());
            }
            if (typemsg.Text && typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    sb.AppendLine(GenerateSendMessageDocumentsMarkup());
            }
            if (typemsg.Document && typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    sb.AppendLine(GenerateSendMessageTextMarkup());
            }
            if (typemsg.Text && !typemsg.Document && !typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    sb.AppendLine(GenerateSendMessagePhotosMarkup());
                if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    sb.AppendLine(GenerateSendMessageDocumentsMarkup());
            }
            if (!typemsg.Text && typemsg.Document && !typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    sb.AppendLine(GenerateSendMessagePhotosMarkup());
                if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    sb.AppendLine(GenerateSendMessageTextMarkup());
            }
            if (!typemsg.Text && !typemsg.Document && typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    sb.AppendLine(GenerateSendMessageDocumentsMarkup());
                if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    sb.AppendLine(GenerateSendMessageTextMarkup());
            }
            if (!typemsg.Text && !typemsg.Document && !typemsg.Photo)
            {
                if (!string.IsNullOrEmpty(ButtonProperty.Documents[0].Path))
                    sb.AppendLine(GenerateSendMessageDocumentsMarkup());
                if (!string.IsNullOrEmpty(ButtonProperty.Text))
                    sb.AppendLine(GenerateSendMessageTextMarkup());
                if (!string.IsNullOrEmpty(ButtonProperty.Photos[0].Path))
                    sb.AppendLine(GenerateSendMessagePhotosMarkup());
            }

            return sb.ToString();
        }

        private string GenerateSendMessageDocuments(bool isInline = true)
        {
            var sb = new StringBuilder();

            foreach (var document in ButtonProperty.Documents)
            {
                if (File.Exists(document.Path))
                {
                    if (isInline)
                        sb.AppendLine("\t" +
                                      (ResourceFunc.BotSendDocument.Replace("PATH", document.Path))
                                      .Replace("name", document.Caption));
                    else
                        sb.AppendLine("\t" +
                                      (InlineButtonLiterals.SendDocument.Replace("call.", "").Replace("PATH", document.Path))
                                      .Replace("name", document.Caption));

                }
            }

            return sb.ToString();
        }

        private string GenerateSendMessagePhotos(bool isInline = true)
        {
            var sb = new StringBuilder();

            foreach (var photo in ButtonProperty.Photos)
            {
                if (File.Exists(photo.Path))
                {
                    if (isInline)
                        sb.AppendLine("\t" +
                                  (ResourceFunc.BotSendPhoto.Replace("PATH", photo.Path))
                                  .Replace("name", photo.Caption));
                    else
                        sb.AppendLine("\t" +
                                  (InlineButtonLiterals.SendPhoto.Replace("call.", "").Replace("PATH", photo.Path))
                                  .Replace("name", photo.Caption));
                }
            }

            return sb.ToString();
        }

        private string GenerateSendMessageText(bool isInline = true)
        {
            var sb = new StringBuilder();
            if (isInline)
                sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("name", ButtonProperty.Text));
            else
                sb.AppendLine("\t" + InlineButtonLiterals.SendText.Replace("call.", "").Replace("name", ButtonProperty.Text));
            return sb.ToString();
        }

        private string GenerateSendMessageDocumentsMarkup(bool isMarkup = true)
        {
            var sb = new StringBuilder();

            foreach (var document in ButtonProperty.Documents)
            {
                if (File.Exists(document.Path))
                {
                    if (isMarkup)
                        sb.AppendLine("\t" +
                                      (ResourceFunc.BotSendDocument.Replace("PATH", document.Path))
                                      .Replace("name", document.Caption));
                    else
                        sb.AppendLine("\t" +
                                      (MarkupButtonLiterals.SendDocument.Replace("PATH", document.Path))
                                      .Replace("name", document.Caption));

                }
            }

            return sb.ToString();
        }

        private string GenerateSendMessagePhotosMarkup(bool isMarkup = true)
        {
            var sb = new StringBuilder();

            foreach (var photo in ButtonProperty.Photos)
            {
                if (File.Exists(photo.Path))
                {
                    if (isMarkup)
                        sb.AppendLine("\t" +
                                  (ResourceFunc.BotSendPhoto.Replace("PATH", photo.Path))
                                  .Replace("name", photo.Caption));
                    else
                        sb.AppendLine("\t" +
                                  (MarkupButtonLiterals.SendPhoto.Replace("PATH", photo.Path))
                                  .Replace("name", photo.Caption));
                }
            }

            return sb.ToString();
        }

        private string GenerateSendMessageTextMarkup(bool isMarkup = true)
        {
            var sb = new StringBuilder();
            if (isMarkup)
                sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("name", ButtonProperty.Text));
            else
                sb.AppendLine("\t" + MarkupButtonLiterals.SendText.Replace("name", ButtonProperty.Text));
            return sb.ToString();
        }

        public string GenerateButtons()
        {
            return GeneratorButtons.GetCodeInlineButtons(InlineButtons, ButtonProperty.CountButtonInLine) +
                   "\n" + GeneratorButtons.GetCodeMarkupButtons(MarkupButtons, ButtonProperty.CountButtonInLine);
        }
    }

}

