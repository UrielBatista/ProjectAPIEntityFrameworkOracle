using System;
using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class Pessoas
    {
        [Key]
        public string Id_Pessoas { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public float Pessoas_Calc_Number { get; set; }

        public DateTime DataHora { get; set; }
    }
}
