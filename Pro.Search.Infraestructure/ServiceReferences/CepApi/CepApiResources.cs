using Flurl;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.ServiceReferences.CepApi
{
    internal sealed class CepApiResources : ICepApiResources
    {
        private readonly HttpClient httpClient;

        public CepApiResources(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        Task<object> ICepApiResources.GetLocalization(string cep)
        {
            _ = cep ?? throw new ArgumentNullException(nameof(cep));

            return CreateTask();

            async Task<object> CreateTask()
            {
                var url = new Url("https://viacep.com.br")
                    .AppendPathSegment("ws")
                    .AppendPathSegment(cep)
                    .AppendPathSegment("json/");

                using var response = await httpClient.GetAsync(url).ConfigureAwait(false);

                _ = response.EnsureSuccessStatusCode().Content;

                return await Task.FromResult(response.Content).ConfigureAwait(false);
            }
        }
    }
}
