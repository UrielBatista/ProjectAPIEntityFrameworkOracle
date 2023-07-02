using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.GraphQL.Types;
using System.Linq;

namespace Pro.Search.Infraestructure.GraphQL.Queries
{
    internal class PersonQuery : ObjectGraphType
    {
        public PersonQuery(ISystemReadDBContext dbContext)
        {
            var queryArgs = new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id_Pessoas", Description = "Pessoas Id" });
            this.Name = "Query";

            _ = this.Field<ListGraphType<FoodsTypes>>("food", "Returns a list of foods", queryArgs, resolve: context => dbContext.Food.Where(p => p.Id_Food == context.GetArgument("id_Food", " ")).ToList());
            _ = this.Field<PersonsTypes>("pessoas", "Returns a list of persons", queryArgs, resolve: context => dbContext.Pessoas.Include(c => c.ComidaComprada).FirstOrDefault(p => p.Id_Pessoas == context.GetArgument("id_Pessoas", " ")));
        }
    }
}
