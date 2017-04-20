using App.Domain.Interfaces.Messages;

namespace App.Domain.Interfaces.Commands
{
    public interface ICommandHandler<in T> : IHandler<T> where T : ICommand
    {
    }
}
