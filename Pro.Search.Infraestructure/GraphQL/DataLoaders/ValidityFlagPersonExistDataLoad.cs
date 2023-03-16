using GreenDonut;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using AutoMapper;
using System.Linq;
using ServiceStack;

namespace Pro.Search.Infraestructure.GraphQL.DataLoaders
{
    public class ValidityFlagPersonExistDataLoad : BatchDataLoader<RequestIdsDto, ValidityFlagDto>
    {
        private readonly IPersonsRepository _repository;
        private readonly IMapper _mapper;

        public ValidityFlagPersonExistDataLoad(
            IPersonsRepository repository,
            IMapper mapper,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;
            _mapper = mapper;
        }

        protected override async Task<IReadOnlyDictionary<RequestIdsDto, ValidityFlagDto>> LoadBatchAsync(IReadOnlyList<RequestIdsDto> keys, CancellationToken cancellationToken)
        {
            var idsPersonAndFood = keys
            .Select(k => new { k.IdPerson, k.IdFood })
            .Distinct()
            .ToList();

            var result = new ValidityFlagDto(default, default);

            var data = await _repository.FindPersonPurcashFood(
                idsPersonAndFood.Select(x => x.IdPerson).FirstOrDefault(), 
                cancellationToken).ConfigureAwait(false);

            if (data != null)
            {
                result.ExistPerson = true;

                var checkIfExistFoodPurcachedInPerson = data.ComidaComprada.ToArray();

                if (checkIfExistFoodPurcachedInPerson.Any())
                {
                    result.ExistFood = true;
                }
                else
                {
                    result.ExistFood = false;
                }
            }
            else
            {
                result.ExistPerson = false;
                result.ExistFood = false;
            }

            var fruitQuantities = new Dictionary<RequestIdsDto, ValidityFlagDto>
            {
                { keys.FirstOrDefault(), result }
            };
            return fruitQuantities;
        }
    }
}
