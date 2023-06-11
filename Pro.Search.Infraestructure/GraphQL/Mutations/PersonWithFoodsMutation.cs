using AppAny.HotChocolate.FluentValidation;
using AutoMapper;
using HotChocolate;
using HotChocolate.Types;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Validators;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class PersonWithFoodsMutation
    {
        private readonly IMapper mapper;

        public PersonWithFoodsMutation(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<PersonWithFoodsDto> UpdateOrCreatePersonWithFoods(
            [UseFluentValidation, UseValidator<PersonWithFoodsDtoValidator>(
            IncludeRuleSets = new[] { "GraphQLPersonWithFoods" })] PersonWithFoodsDto personsWithFoods,
            [Service] ISystemDBContext context,
            CancellationToken cancellationToken)
        {
            _ = context;
            _ = mapper;
            _ = cancellationToken;

            return await Task.FromResult(personsWithFoods);
        }
    }
}
