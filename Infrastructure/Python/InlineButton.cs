
using System;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class InlineButton
{
    public int Position
    {
        get { return Position;}
        set { if (value < 0) throw new ArgumentException(); }
    }

    public bool IsURL { get; set; }
    public string IdOrURL { get; set; }
    public string Text { get; set; }

    public override string ToString()
    {
        return "types.InlineKeyboardButton(text='" + Text + "'" + ((IsURL) ? "url='" : "callback_data='") + IdOrURL + "'),";
    }
}