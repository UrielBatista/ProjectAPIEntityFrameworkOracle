using Pro.Search.Infraestructure.ServiceReferences.CepApi.Dtos;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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

        public static HttpClient ApiClient { get; set; }

        Task<CepDataResultDto> ICepApiResources.GetLocalization(string cep, CancellationToken cancellationToken)
        {
            _ = cep ?? throw new ArgumentNullException(nameof(cep));

            return CreateTask();

            async Task<CepDataResultDto> CreateTask()
            {
                var url = $"http://viacep.com.br/ws/{cep}/json/";
                ApiClient = new HttpClient();
                ApiClient.BaseAddress = new Uri(url);
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await ApiClient.GetAsync(ApiClient.BaseAddress, cancellationToken).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        CepDataResultDto cepReceiver = await response.Content.ReadAsAsync<CepDataResultDto>();
                        return cepReceiver;
                    }
                }
                return new CepDataResultDto();
            }
        }
    }
}
