using AutoMapper;
using BuldBlocks.Domain.Commons;
using JIgor.Projects.ListPicker;
using MassTransit;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.CreateOrUpdatePickerListResponses;

namespace Pro.Search.PersonCommands
{
    public class CreatePersonPickerCommandHandler : ICommandHandler<CreatePersonPickerCommand, CreateOrUpdatePickerListResponses>
    {
        private readonly ISystemDBContext _context;
        private readonly IPersonsRepository repository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publish;

        public CreatePersonPickerCommandHandler(
            ISystemDBContext _context,
            IMapper mapper, IPersonsRepository repository, IPublishEndpoint publish)
        {
            this._context = _context;
            this.mapper = mapper;
            this.repository = repository;
            this.publish = publish;
        }

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
