using AutoMapper;
using BuldBlocks.Domain.Commons;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonCommands.Queries
{
    public class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, PersonDto>
    {
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public UpdatePersonCommandHandler(ContextDB context, IPessoasRepository repository, IMapper mapper)
        {
            _context = context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PersonDto> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var personDb = await this.repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, cancellationToken).ConfigureAwait(false);

            _ = personDb ?? throw new ArgumentNullException(paramName: nameof(personDb), message: "Argumento nao pode ser nulo");

            var dateTime = UpdatePersonCommandHandler.DefineLocalTime(request);
            personDb.Nome = request.PersonDto.Pessoas.Nome;
            personDb.Sobrenome = request.PersonDto.Pessoas.Sobrenome;
            personDb.Pessoas_Calc_Number = request.PersonDto.Pessoas.Pessoas_Calc_Number;
            personDb.DataHora = dateTime;
            await _context.SaveChangesAsync();
            return request.PersonDto;
        }

        private static DateTime DefineLocalTime(UpdatePersonCommand request)
        {
            DateTime localDate = DateTime.Now;
            request.PersonDto.Pessoas.DataHora = localDate;
            return localDate;
        }
    }
}
