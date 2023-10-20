using HotChocolate.Types;

namespace Pro.Search.Infraestructure.GraphQL.Directives
{
    public class DistinctDirective
    {
        [DirectiveType(DirectiveLocation.Field)]
        [ConfigureDirectivesOfDistinct]
        public class Distinct { }
    }
}