using MediatR;

namespace BuldBlocks.Domain.Commons
{
    public interface IQueryHandler<in TQuery> : IQueryHandler<IQuery, bool>
        where TQuery : IQuery
    {
    }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
