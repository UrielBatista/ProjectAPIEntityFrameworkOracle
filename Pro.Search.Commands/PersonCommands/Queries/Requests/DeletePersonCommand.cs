﻿using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;

namespace Pro.Search.PersonCommands.Queries.Requests
{
    public class DeletePersonCommand : IRequest<List<Pessoas>>
    {
        public DeletePersonCommand(string Id_Pessoas)
        {
            this.Id_Pessoas = Id_Pessoas;
        }

        public string Id_Pessoas { get; set; }
    }
}
