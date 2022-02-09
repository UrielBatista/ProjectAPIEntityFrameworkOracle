using System;
using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class Food
    {
        [Key]
        public string Id_Food { get; set; }

        public string Name_Food { get; set; }

        public string Locale_Purcache_Food { get; set; }

        public string Id_Pessoas_References { get; set; }

        public decimal Price_Food { get; set; }
    }
}
