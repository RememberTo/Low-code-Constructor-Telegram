using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatbotConstructorTelegram.Infrastructure.Python.Literals;
using ChatbotConstructorTelegram.Model.ViewData.BotView.Button;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class GeneratorButtons
{
    public static string GetCodeInlineButtons(List<InlineButtonProperty> inlineButtons, int countButtonInLine)
    {
        var sb = new StringBuilder();

        sb.Append("\t"+InlineButtonLiterals.InizializeOpen);

        if (countButtonInLine <= 0) countButtonInLine = 1;

        for (var i = 0; i < inlineButtons.Count; i += countButtonInLine)
        {
            sb.Append(InlineButtonLiterals.LinebuttonsOpen);

            if (inlineButtons.Count - i >= countButtonInLine)
            {
                for (var j = i; j < countButtonInLine + i; j++)
                {
                    sb.Append(GetCodeInlineButton(inlineButtons[j]));
                }
            }
            else
            {
                for (var j = i; j < inlineButtons.Count; j++)
                {
                    sb.Append(GetCodeInlineButton(inlineButtons[j]));
                }
            }

            sb.Append(InlineButtonLiterals.LinebuttonsClose);
        }

        sb.Append(InlineButtonLiterals.InizializeClose);

        return sb.ToString();
    }

    private static string GetCodeInlineButton(InlineButtonProperty inlineButton)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(inlineButton.URL))
            sb.Append("\t" + InlineButtonLiterals.ViewButton.Replace("TEXT", inlineButton.Name)
                .Replace("NONE", inlineButton.URL)
                .Replace("UNIQUE_ID", inlineButton.UniqueId));
        else
            sb.Append("\t" + InlineButtonLiterals.ViewButton.Replace("TEXT", inlineButton.Name)
                .Replace("'NONE'", "None")
                .Replace("UNIQUE_ID", inlineButton.UniqueId));

        return sb.ToString();
    }

    public static string GetCodeMarkupButtons(List<MarkupButtonProperty> markupButtons, int countButtonInLine)
    {
        var sb = new StringBuilder();

        sb.Append("\t"+MarkupButtonLiterals.InizializeOpen);

        if (countButtonInLine <= 0) countButtonInLine = 1;

        for (var i = 0; i < markupButtons.Count; i += countButtonInLine)
        {
            sb.Append(MarkupButtonLiterals.LinebuttonsOpen);

            if (markupButtons.Count - i >= countButtonInLine)
            {
                for (var j = i; j < countButtonInLine + i; j++)
                {
                    sb.Append(GetCodeMarkupButton(markupButtons[j]));
                }
            }
            else
            {
                for (var j = i; j < markupButtons.Count; j++)
                {
                    sb.Append(GetCodeMarkupButton(markupButtons[j]));
                }
            }

            sb.Append(MarkupButtonLiterals.LinebuttonsClose);
        }

        sb.Append(MarkupButtonLiterals.InizializeClose);

        return sb.ToString();
    }

    private static string GetCodeMarkupButton(MarkupButtonProperty markupButton)
    {
        return "\t" + MarkupButtonLiterals.ViewButton.Replace("TEXT", markupButton.Name);
    }
}