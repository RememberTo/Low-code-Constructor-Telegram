
using System;
using System.Text;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;
using ChatbotConstructorTelegram.Model.ViewData.BotView.PropertiesView;
using ChatbotConstructorTelegram.Resources;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

public class InlineButton
{
    #region Const Field

    private const string INIZIALIZE_OPEN = "markup_inline = types.InlineKeyboardMarkup(row_width = 2, inline_keyboard =[";
    private const string INIZIALIZE_CLOSE = "])";
    private const string LINEBUTTONS_OPEN = "inline_keyboard=[[";
    private const string LINEBUTTONS_CLOSE = "],";

    #endregion

    #region Variable field

    private string ViewButton = "types.InlineKeyboardButton(text='TEXT', url='NONE', callback_data='UNIQUE_ID'),";
    private string SendText = "await bot.send_message(call.message.chat.id, text='name', caption='TEXT' reply_markup=markup_inline)";
    private string SendPhoto = "await bot.send_photo(call.message.chat.id, photo='PHOTO', caption='name' reply_markup = markup_inline)";
    private string SendDocument = "await bot.send_document(call.message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_inline)";
    private string DecoratorFunction = "@dp.callback_query_handler(text='UNIQUEID')";
    private string HeadFunction = "async def NAME(call: types.CallbackQuery):";

    #endregion

    public InlineButtonProperty ButtonProperty { get; set; }
    public int CountNestedButton { get; set; }

    public InlineButton(InlineButtonProperty buttonProperty)
    {
        ButtonProperty = buttonProperty ?? throw new ArgumentNullException(nameof(buttonProperty));
        CountNestedButton = buttonProperty.Children.Count;
    }

    public string GenerateFunc()
    {
        var sb = new StringBuilder();

        sb.AppendLine(DecoratorFunction.Replace("UNIQUEID", ButtonProperty.UniqueId));
        sb.AppendLine(HeadFunction.Replace("NAME", ButtonProperty.Name+ButtonProperty.UniqueId));
        sb.AppendLine(GenerateButton());
        sb.AppendLine(GenerateSendMessages());

        return sb.ToString();
    }

    private string? GenerateSendMessages()
    {
        var sb = new StringBuilder();

        //switch (ButtonProperty.AtachButtonMessage.GetTrueTypeMessage())
        //{
        //    case "Text":
        //        if (string.IsNullOrEmpty(ButtonProperty.Document.Path) == false)
        //            sb.AppendLine("\t" + (ResourceFunc.BotSendDocument.Replace("message", "call.message").Replace("PATH", ButtonProperty.Document.Path)).Replace("name", ButtonProperty.Document.Caption));
        //        if (string.IsNullOrEmpty(ButtonProperty.Photo.Path) == false)
        //            sb.AppendLine("\t" + (ResourceFunc.BotSendPhoto.Replace("message", "call.message").Replace("PATH", ButtonProperty.Photo.Path)).Replace("name", ButtonProperty.Photo.Caption));
        //        if (string.IsNullOrEmpty(ButtonProperty.Text) == false)
        //            sb.AppendLine("\t" + SendText.Replace("name", ButtonProperty.Text));
        //        break;
        //    case "Photo":
        //        if (string.IsNullOrEmpty(ButtonProperty.Text) == false)
        //            sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("message", "call.message").Replace("name", ButtonProperty.Text));
        //        if (string.IsNullOrEmpty(ButtonProperty.Document.Path) == false)
        //            sb.AppendLine("\t" + (ResourceFunc.BotSendDocument.Replace("message", "call.message").Replace("PATH", ButtonProperty.Document.Path)).Replace("name", ButtonProperty.Document.Caption));
        //        if (string.IsNullOrEmpty(ButtonProperty.Photo.Path) == false)
        //            sb.AppendLine("\t" + (SendPhoto.Replace("PATH", ButtonProperty.Photo.Path)).Replace("name", ButtonProperty.Photo.Caption));
        //        break;
        //    case "Document":
        //        if (string.IsNullOrEmpty(ButtonProperty.Text) == false)
        //            sb.AppendLine("\t" + ResourceFunc.BotSendMessage.Replace("message", "call.message").Replace("name", ButtonProperty.Text));
        //        if (string.IsNullOrEmpty(ButtonProperty.Document.Path) == false)
        //            sb.AppendLine("\t" + (SendDocument.Replace("PATH", ButtonProperty.Document.Path)).Replace("name", ButtonProperty.Document.Caption));
        //        if (string.IsNullOrEmpty(ButtonProperty.Photo.Path) == false)
        //            sb.AppendLine("\t" + (ResourceFunc.BotSendPhoto.Replace("message", "call.message").Replace("PATH", ButtonProperty.Photo.Path)).Replace("name", ButtonProperty.Photo.Caption));
        //        break;
        //    case "Default":
        //        break;
        //}
        return sb.ToString();
    }

    public string GenerateButton()
    {
        var sb = new StringBuilder();

        sb.Append(INIZIALIZE_OPEN);

        for (var i = 0; i < ButtonProperty.Children.Count; i += ButtonProperty.CountButtonInLine)
        {
            sb.Append(LINEBUTTONS_OPEN);

            if (ButtonProperty.Children.Count - i >= ButtonProperty.CountButtonInLine)
            {
                for (var j = i; j < ButtonProperty.CountButtonInLine + i; j++)
                {
                    GetCodeInlineButton(j);
                }
            }
            else
            {
                for (var j = i; j < ButtonProperty.Children.Count; j++)
                {
                    GetCodeInlineButton(j);
                }
            }

            sb.Append(LINEBUTTONS_CLOSE);
        }

        sb.Append(INIZIALIZE_CLOSE);

        return sb.ToString();
    }

    public string GetCodeInlineButton(int j)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(ButtonProperty.Children[j].URL))
            sb.Append("\t"+ViewButton.Replace("TEXT", ButtonProperty.Children[j].Name)
                .Replace("NONE", ButtonProperty.Children[j].URL)
                .Replace("UNIQUE_ID", ButtonProperty.Children[j].UniqueId));
        else
            sb.Append("\t"+ViewButton.Replace("TEXT", ButtonProperty.Children[j].Name)
                .Replace("'NONE'", "None")
                .Replace("UNIQUE_ID", ButtonProperty.Children[j].UniqueId));

        return sb.ToString();
    }
}