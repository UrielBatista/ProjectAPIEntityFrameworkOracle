using GraphQL.Types;
using Pro.Search.PersonDomains.PersonEngine.Entities;

namespace Pro.Search.PersonDomains.PersonEngine.GraphQL.Types
{
    public class FoodsTypes : ObjectGraphType<Food>
    {
        public FoodsTypes()
        {
            _ = this.Field(v => v.Id_Food).Description("FOOD.ID_FOOD");
            _ = this.Field(v => v.Name_Food).Description("FOOD.NAME_FOOD");
            _ = this.Field(v => v.Locale_Purcache_Food).Description("FOOD.LOCALE_PURCACHE_FOOD");
            _ = this.Field(v => v.Id_Pessoas_References).Description("FOOD.ID_PESSOAS_REFERENCES");
            _ = this.Field(v => v.Price_Food).Description("FOOD.PRICE_FOOD");
        }
    }
}
