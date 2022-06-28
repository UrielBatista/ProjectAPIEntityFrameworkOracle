using BuldBlocks.Domain.Commons;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System.Collections.Generic;

namespace Pro.Search.Commands.PersonCommands
{
    public class CreatePersonPickerCommand : ICommand<CreateOrUpdatePickerListResponses>
    {
        public CreatePersonPickerCommand(IEnumerable<string> persons)
        {
            this.Persons = persons;
        }

        public IEnumerable<string> Persons { get; set; }
    }
}
