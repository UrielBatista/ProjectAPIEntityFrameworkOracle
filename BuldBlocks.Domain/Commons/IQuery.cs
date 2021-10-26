using MediatR;

namespace BuldBlocks.Domain.Commons
{
    public interface IQuery : IQuery<bool>
    {
    }

    public interface IQuery<out T> : IRequest<T>
    {
    }
}
