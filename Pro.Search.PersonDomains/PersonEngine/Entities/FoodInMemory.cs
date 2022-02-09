using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class FoodInMemory
    {
        [Key]
        public Guid Id_Food { get; private set; }

        public string Name_Food { get; set; }

        public string Locale_Purcache_Food { get; set; }

        public string Id_Pessoas_References { get; set; }

        public decimal Price_Food { get; set; }

        public FoodInMemory()
        {
            Id_Food = Guid.NewGuid();
        }
    }
}
