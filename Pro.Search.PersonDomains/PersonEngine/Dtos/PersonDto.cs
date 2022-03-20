﻿using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Pro.Search.PersonDomains.PersonEngine.Dtos
{
    [ExcludeFromCodeCoverage]
    [JsonObject(ItemRequired = Required.AllowNull)]
    public class PersonDto
    {
        public PersonsInfoDto Pessoas {get; set;}
    }
}
