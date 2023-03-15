
using System;
using System.Text;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;

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

        sb.AppendLine(GenerateButton());

        sb.AppendLine("await message.answer('"+ButtonProperty.Text+"', reply_markup=markup_inline)");

        return sb.ToString();
    }

    private string GenerateButton()
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
            sb.Append(ViewButton.Replace("TEXT", ButtonProperty.Children[j].Name)
                .Replace("NONE", ButtonProperty.Children[j].URL)
                .Replace("UNIQUE_ID", ButtonProperty.Children[j].UniqueId));
        else
            sb.Append(ViewButton.Replace("TEXT", ButtonProperty.Children[j].Name)
                .Replace("'NONE'", "None")
                .Replace("UNIQUE_ID", ButtonProperty.Children[j].UniqueId));

        return sb.ToString();
    }
}