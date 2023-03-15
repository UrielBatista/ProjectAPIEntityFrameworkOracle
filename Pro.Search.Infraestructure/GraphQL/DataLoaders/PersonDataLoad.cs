using GreenDonut;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using AutoMapper;
using System.Linq;
using ServiceStack;

namespace Pro.Search.Infraestructure.GraphQL.DataLoaders
{
    public class PersonDataLoad : BatchDataLoader<string, List<PersonsAllInfoDto>>
    {
        private readonly IPersonsRepository _repository;
        private readonly IMapper _mapper;

        public PersonDataLoad(
            IPersonsRepository repository,
            IMapper mapper,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected override async Task<IReadOnlyDictionary<string, List<PersonsAllInfoDto>>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var values = keys.ToArray();
            var persons = await _repository.FindAllAsyncPersonWithId(values, cancellationToken);

            var data = new List<PersonsAllInfoDto>();
            foreach (var i in persons)
            {
                data.Add(_mapper.Map<Persons, PersonsAllInfoDto>(i));
            }

            return data.ToDictionary(x => x.Id_Pessoas, x => x.InList());
        }
    }
}
