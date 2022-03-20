using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class FoodInMemory
    {
        [Key]
        public string Id_Food { get; private set; }

        public string Name_Food { get; set; }

        public string Locale_Purcache_Food { get; set; }

        public string Id_Pessoas_References { get; set; }

        public decimal Price_Food { get; set; }

        public FoodInMemory(string id_food)
        {
            Id_Food = id_food;
        }
    }
}
