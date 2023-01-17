using System;

namespace ChatbotConstructorTelegram.Infrastructure.Python;

internal class KeyboardButton
{
    public int Position
    {
        get { return Position;}
        set { if (value < 0) throw new ArgumentException(); }
    }

    public string Text { get; set; }

    public override string ToString()
    {
        return "types.KeyboardButton('" + Text + "'),";
    }
}