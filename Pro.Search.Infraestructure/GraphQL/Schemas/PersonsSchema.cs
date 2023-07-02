using GraphQL.Types;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.GraphQL.Queries;
using System;

namespace Pro.Search.Infraestructure.GraphQL.Schemas
{
    public class PersonSchema : Schema
    {
        public PersonSchema(IServiceProvider serviceProvider, ISystemReadDBContext dbContext)
            : base(serviceProvider)
        {
            this.Query = new PersonQuery(dbContext);
        }
    }
}
