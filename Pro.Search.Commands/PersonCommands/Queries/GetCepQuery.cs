using MediatR;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetCepQuery : IRequest<object>
    {
        public GetCepQuery(string cep)
        {
            this.Cep = cep;
        }

        public string Cep { get; set; }
    }
}
