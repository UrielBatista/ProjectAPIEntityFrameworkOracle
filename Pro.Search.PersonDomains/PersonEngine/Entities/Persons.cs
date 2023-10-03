﻿using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pro.Search.PersonDomains.PersonEngine.Entities
{
    public class Persons
    {
        [Key]
        public string Id_Pessoas { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public decimal Pessoas_Calc_Number { get; set; }

        public DateTime DataHora { get; set; }

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Food> ComidaComprada { get; set; }
    }
}
