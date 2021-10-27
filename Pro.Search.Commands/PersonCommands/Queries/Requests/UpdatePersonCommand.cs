﻿using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

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
