using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using ServiceStack;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.GraphQL.Subscriptions
{
    public class PersonSubscriptions
    {
        [Subscribe]
        [Topic("CreatedGraphQLPerson")]
        public GraphQLPersonsInfoDto PersonGraphQLAdded([EventMessage] GraphQLPersonsInfoDto graphQLPersonsInfoDto) =>
            graphQLPersonsInfoDto;


        [Subscribe]
        [Topic("UpdatedGraphQLPerson")]
        public GraphQLPersonsInfoDto PersonGraphQLUpdated([EventMessage] GraphQLPersonsInfoDto graphQLPersonsInfoDto) =>
            graphQLPersonsInfoDto;
    }
}
