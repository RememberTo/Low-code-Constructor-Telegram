using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class MarkupHelper
{
    private const string REPLY = "markup_reply = types.ReplyKeyboardMarkup(resize_keyboard=True, keyboard=[";
    private const string INLINE = "markup_inline = types.InlineKeyboardMarkup(row_width = 2, inline_keyboard =[";
    private List<InlineButton> Buttons { get; set; }
    private List<KeyboardButton> Buttonss { get; set; }
    public string GeneratedMarkup<T>(T list, TypeButton typeButton)
    {
        if (typeButton == TypeButton.InlineButton)
        {
            return CreateCodeButton(INLINE);
        }
        //else (typeButton == TypeButton.KeyboardButton);
        //{
        return CreateCodeButton(REPLY);
        //}
    }

    private string CreateCodeButton(string type)
    {
        var uniqueElements = GetUniquePosition();
        var buttonLines = new StringBuilder();

        foreach (var pos in uniqueElements)
        {
            var sb = new StringBuilder();

            foreach (var button in Buttons.Where(button => pos == button.Position))
            {
                sb.Append(button.ToString() + ",");
            }

            buttonLines.Append("[");
            buttonLines.Append(sb.ToString());
            buttonLines.Append("],");
        }

        buttonLines.Insert(0, type);
        buttonLines.Append("])");

        return buttonLines.ToString();
    }

    private List<int> GetUniquePosition()
    {
        var list = new List<int>();
        foreach (var but in Buttons)
        {
            list.Add(but.Position);
        }

        return list.Distinct().ToList();
    }
}