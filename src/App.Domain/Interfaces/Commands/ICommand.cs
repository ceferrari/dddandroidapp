using App.Domain.Interfaces.Messages;

namespace App.Domain.Interfaces.Commands
{
    public interface ICommand : IMessage
    {
        int ExpectedVersion { get; }
    }
}
