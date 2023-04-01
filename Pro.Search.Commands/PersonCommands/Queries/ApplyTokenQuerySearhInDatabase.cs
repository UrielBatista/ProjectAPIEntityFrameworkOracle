using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class ApplyTokenQuerySearhInDatabase : IRequest<TokenRequestResponses>
    {

        public ApplyTokenQuerySearhInDatabase(TokenLoginRequestDto request)
        {
            this.Username = request.Username;
            this.Password = request.Password;
            
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
