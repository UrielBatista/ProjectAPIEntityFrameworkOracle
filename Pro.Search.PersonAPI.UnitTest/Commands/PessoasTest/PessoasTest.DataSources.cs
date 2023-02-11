using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasTest
{
    public sealed partial class PessoasTest
    {
        private static class DataSouces
        {
            public static IEnumerable<object[]> GetAllPersonsDataSource { get; } = new[]
            {
                new object[]
                {
                    new List<Persons>
                    {
                        new Persons
                        {
                            Id_Pessoas = "1234",
                        },
                    },
                }
            };

            public static IEnumerable<object[]> MediaCalcDataSource { get; } = new[]
            {
                new object[]
                {
                    new List<decimal>
                    {
                        11.2M,
                        12.89M,
                    },
                }
            };

            public static IEnumerable<object[]> GetOnePersonDataSource { get; } = new[]
            {
                new object[]
                {
                    "0001",
                    new Persons
                    {
                        Id_Pessoas = "0001",
                    },
                }
            };

            public static IEnumerable<object[]> GetPersonPurcashFood { get; } = new[]
            {
                new object[]
                {
                    "5757",
                    new Persons
                    {
                        Id_Pessoas = "5757",
                    },
                }
            };

            public static IEnumerable<object[]> CreatePersonDataSource { get; } = new[]
            {
                new object[]
                {
                    new PersonDto
                    {
                        Pessoas = new PersonsInfoDto
                        {
                            Id_Pessoas = "987456",
                            Nome = "CreateTestePerson",
                            Sobrenome = "CreateTesteSobrenomePerson",
                            Pessoas_Calc_Number = 11.2M,
                            DataHora = DateTime.Now,
                        },
                    },
                    new Persons
                    {
                        Id_Pessoas = "987456",
                        Nome = "CreateTestePerson",
                        Sobrenome = "CreateTesteSobrenomePerson",
                        Pessoas_Calc_Number = 11.2M,
                        DataHora = DateTime.Now,
                    },
                }
            };

            public static IEnumerable<object[]> UpdatePersonDataSource { get; } = new[]
            {
                new object[]
                {
                    new PersonDto
                    {
                        Pessoas = new PersonsInfoDto
                        {
                            Id_Pessoas = "25752",
                            Nome = "UpdateTestePerson",
                            Sobrenome = "UpdateTesteSobrenomePerson",
                            Pessoas_Calc_Number = 12.6M,
                            DataHora = DateTime.Now
                        },
                    },
                    new Persons
                    {
                        Id_Pessoas = "987456",
                        Nome = "CreateTestePerson",
                        Sobrenome = "CreateTesteSobrenomePerson",
                        Pessoas_Calc_Number = 11.2M,
                        DataHora = DateTime.Now,
                    },
                }
            };

            public static IEnumerable<object[]> DeletePersonDataSource { get; } = new[]
            {
                new object[]
                {
                    "6548",
                    new List<Persons>
                    {
                        new Persons
                        {
                            Id_Pessoas = "6548",
                            Nome = "TesteHandle1",
                            Sobrenome = "TesteHandleSobrenome1",
                            Pessoas_Calc_Number = 45.2M,
                            DataHora = DateTime.Now,
                            ComidaComprada = new List<Food>
                            {
                                new Food
                                {
                                    Id_Food = "88585",
                                    Name_Food = "TesteHandleFood1",
                                    Locale_Purcache_Food = "TestandoLocalHandle1",
                                    Id_Pessoas_References = "6548",
                                     Price_Food = 44.6M
                                },
                            },
                        },
                    },
                }
            };

            public static IEnumerable<object[]> DeletePersonBadRequestDataSource { get; } = new[]
            {
                new object[]
                {
                    "6548",
                    new List<Persons>
                    {
                        new Persons
                        {
                            Id_Pessoas = "6548",
                            Nome = "TesteHandle1",
                            Sobrenome = "TesteHandleSobrenome1",
                            Pessoas_Calc_Number = 45.2M,
                            DataHora = DateTime.Now,
                            ComidaComprada = new List<Food>(),
                        },
                    },
                }
            };
        }
    }
}
