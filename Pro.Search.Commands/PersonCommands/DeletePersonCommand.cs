using MediatR;
using Pro.Search.PersonDomains.PersonEngine.OneOf;

namespace Pro.Search.PersonCommands
{
    public class DeletePersonCommand : IRequest<DeleteResponses>
    {
        public DeletePersonCommand(string Id_Pessoas)
        {
            this.Id_Pessoas = Id_Pessoas;
        }

        public string Id_Pessoas { get; set; }
    }
}
