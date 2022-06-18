using BuldBlocks.Domain.Commons;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetCepQuery : IQuery<object>
    {
        public GetCepQuery(string cep)
        {
            this.Cep = cep;
        }

        public string Cep { get; set; }
    }
}
