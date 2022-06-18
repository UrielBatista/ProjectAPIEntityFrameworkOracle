using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.ServiceReferences.CepApi;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetCepQueryHandler : IQueryHandler<GetCepQuery, object>
    {
        private readonly ICepApiResources cepApi;

        public GetCepQueryHandler(ICepApiResources cepApi)
        {
            this.cepApi = cepApi;
        }

        public async Task<object> Handle(GetCepQuery request, CancellationToken cancellationToken)
        {
            var dataCollection = await this.cepApi.GetLocalization(request.Cep)
                .ConfigureAwait(false);

            return dataCollection;
        }
    }
}
