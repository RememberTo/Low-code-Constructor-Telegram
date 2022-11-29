using System.Windows;
using ChatbotConstructorTelegram.Infastructure.Commands.Base;

namespace ChatbotConstructorTelegram.Infastructure.Commands;

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