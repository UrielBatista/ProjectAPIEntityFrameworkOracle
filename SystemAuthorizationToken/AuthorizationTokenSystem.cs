using Microsoft.Extensions.DependencyInjection;
using SystemAuthorizationToken.Dto;
using System.Text.Json;
using System.Text;

namespace SystemAuthorizationToken
{
    public static class AuthorizationTokenSystem
    {
        public static IHttpClientBuilder AddSystemTokenAuthorization(this IHttpClientBuilder httpClientBuilder)
        {
            var http = new HttpClient();
            var payload = new LoginDto
            {
                Username = "uriel",
                Password = "adm321",
            };

            var jsonSerialize = JsonSerializer.Serialize(payload);
            var data = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");

            var uri = new Uri("https://localhost:5001/api/v3/Person/login");

            var response = http.PostAsync(uri, data)
               .GetAwaiter().GetResult();

            var objectToken = response.Content.ReadAsStringAsync()
                .GetAwaiter().GetResult();

            TokenReturnDto tokenResponse = JsonSerializer.Deserialize<TokenReturnDto>(objectToken)!;

            _ = httpClientBuilder.ConfigureHttpClient(http =>
            {
                _ = http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.token);
            });

            return httpClientBuilder;
        }
    }
}