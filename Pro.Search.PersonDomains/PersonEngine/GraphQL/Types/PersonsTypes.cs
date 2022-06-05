using GraphQL.Types;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.PersonDomains.PersonEngine.GraphQL.Types
{
    public class PersonsTypes : ObjectGraphType<Persons>
    {
        public PersonsTypes()
        {
            _ = this.Field(v => v.Id_Pessoas).Description("PESSOAS.ID_PESSOAS");
            _ = this.Field(v => v.Nome).Description("PESSOAS.NOME");
            _ = this.Field(v => v.Sobrenome).Description("PESSOAS.SOBRENOME");
            _ = this.Field(v => v.Pessoas_Calc_Number).Description("PESSOAS.PESSOAS_CALC_NUMBER");
            _ = this.Field(v => v.DataHora, type: typeof(DateTimeGraphType)).Description("PESSOAS.DATA_HORA");
            _ = this.Field(v => v.ComidaComprada, true, type: typeof(ListGraphType<FoodsTypes>));

        }
    }
}
