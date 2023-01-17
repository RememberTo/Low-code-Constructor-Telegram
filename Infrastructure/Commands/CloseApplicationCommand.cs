using System.Windows;
using ChatbotConstructorTelegram.Infrastructure.Commands.Base;

namespace ChatbotConstructorTelegram.Infrastructure.Commands;

internal class CloseApplicationCommand : Command
{
    public override bool CanExecute(object? parameter)
    {
        return true;
    }

    public override void Execute(object? parameter)
    {
        Application.Current.Shutdown();
    }
}