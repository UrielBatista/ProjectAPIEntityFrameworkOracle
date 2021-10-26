using BuldBlocks.Domain.Commons;
using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;

namespace Pro.Search.PersonCommands.Queries.Requests
{
    public class UpdatePersonCommand : ICommand<PersonDto>
    {
        public UpdatePersonCommand(PersonDto dto)
        {
            this.PersonDto = dto;
        }

        public PersonDto PersonDto { get; set; }
    }
}
