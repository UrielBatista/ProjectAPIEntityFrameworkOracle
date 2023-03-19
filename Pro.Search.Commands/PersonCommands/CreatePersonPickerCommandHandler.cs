using JIgor.Projects.ListPicker;
using MediatR;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdatePickerListResponses;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonPickerCommandHandler : IRequestHandler<CreatePersonPickerCommand, CreateOrUpdatePickerListResponses>
    {
        public async Task<CreateOrUpdatePickerListResponses> Handle(CreatePersonPickerCommand request, CancellationToken cancellationToken)
        {
            var picker = new ListPicker();

            var newUsersList = new List<PersonPickerNewListDto>();
            int count = 0;

            request.Persons.ToList().ForEach(person =>
            {
                var data = new PersonPickerNewListDto
                {
                    Id = count,
                    Name = person,
                };
                count++;

                newUsersList.Add(data);
            });


            var removedValues = picker.PickElements(newUsersList, 2);

            newUsersList.ForEach(x => 
                removedValues.ToList().Remove(x));

            var returnResultSet = new PersonPickerDto
            {
                Values = newUsersList,
                Removeds = removedValues,
            };

            return await Task.FromResult(new Success(returnResultSet)).ConfigureAwait(false);
        }
    }
}
