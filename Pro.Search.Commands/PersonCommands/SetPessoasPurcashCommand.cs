﻿using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;

namespace Pro.Search.Commands.PersonCommands
{
    public class SetPessoasPurcashCommand : ICommand<PersonPurcashDto>
    {
        public SetPessoasPurcashCommand(string id_pessoa, PersonPurcashDto personPurcashDto)
        {
            this.Id_Pessoa = id_pessoa;
            this.PersonPurcashDto = personPurcashDto;
        }

        public string Id_Pessoa { get; set; }

        public PersonPurcashDto PersonPurcashDto { get; set; }
    }
}
