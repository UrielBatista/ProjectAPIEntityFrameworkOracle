using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.PersonDomains.PersonEngine.OneOf
{
    public sealed partial class TokenRequestResponses
    {
        public sealed class Success
        {
            public Success(TokenResponseDto tokenResponseDto)
            {
                this.TokenResponseDto = tokenResponseDto;
            }

            public TokenResponseDto TokenResponseDto { get; set; }
        }
    }
}
