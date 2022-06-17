using System;
using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    public class PersonsInfoDto
    {
        [Key]
        public string Id_Pessoas { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public decimal Pessoas_Calc_Number { get; set; }

        public DateTime DataHora { get; set; }
    }
}
