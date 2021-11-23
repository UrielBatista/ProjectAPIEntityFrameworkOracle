using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;

namespace Pro.Search.Commands.PersonCommands
{
    public class DeleteFoodCommand : IRequest<List<Food>>
    {
        public DeleteFoodCommand(string Id_Food)
        {
            this.Id_Food = Id_Food;
        }

        public string Id_Food { get; set; }
    }
}
