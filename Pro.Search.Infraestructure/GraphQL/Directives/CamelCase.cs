using HotChocolate.Types;

namespace Pro.Search.Infraestructure.GraphQL.Directives
{
    public class CamelCase { }

    [DirectiveType(DirectiveLocation.Field)]
    [ConfigureDirectivesCamelCase]
    public class ToLowerCase { }

    [DirectiveType(DirectiveLocation.Field)]
    [ConfigureDirectivesCamelCase]
    public class ToUpperCase { }
}
