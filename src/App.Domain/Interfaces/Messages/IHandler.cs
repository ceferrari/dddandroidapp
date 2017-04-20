using System.Threading.Tasks;

namespace App.Domain.Interfaces.Messages
{
    public interface IHandler<in T> where T : IMessage
    {
        Task Handle(T message);
    }
}
