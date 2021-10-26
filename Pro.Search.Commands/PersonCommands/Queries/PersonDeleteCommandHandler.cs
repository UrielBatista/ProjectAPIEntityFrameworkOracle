using AutoMapper;
using BuldBlocks.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PersonDomains.PersonEngine.Commands
{
    public class PersonDeleteCommandHandler : ICommandHandler<PersonDeleteCommand, PersonDto>
    {
        private readonly ContextDB _context;
        private readonly IPessoasRepository repository;
        private readonly IMapper mapper;

        public PersonDeleteCommandHandler(ContextDB context, IPessoasRepository repository, IMapper mapper)
        {
            _context = context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<PersonDto> Handle(PersonDeleteCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var pessoa = await this.repository.FindOneAsyncPerson(request.Id_Pessoas, cancellationToken).ConfigureAwait(false);
            if (pessoa == null) return new PersonDto();

            var returnValid = await this.DeletePessoas(request);

            var personDto = new PersonDto
            {
                Pessoas = this.mapper.Map<Pessoas, PessoasInfoDto>(returnValid),
            };

            return personDto;
        }

        private async Task<Pessoas> DeletePessoas(PersonDeleteCommand request)
        {
            var pessoas = await _context.Pessoas.Where(a => a.Id_Pessoas == request.Id_Pessoas).FirstOrDefaultAsync();
            _context.Remove(pessoas);
            await _context.SaveChangesAsync();
            return pessoas;
        }
    }
}
