using Microsoft.VisualStudio.TestPlatform.Common;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasTests
{
    public sealed partial class PessoasTestsClass
    {
        private static class DataSources
        {
            public static IEnumerable<object[]> GetPessoasDataLoadController { get; } = new[]
            {
                new object[]
                {
                    new List<PersonsAllInfoDto>
                    {
                        new PersonsAllInfoDto
                        {
                            Id_Pessoas = "151251",
                            Nome = "TesteGetMethodController1",
                            Sobrenome = "TesteGetMethodController2",
                            Pessoas_Calc_Number = 12.7M,
                            DataHora = DateTime.Now
                        },
                    }
                }
            };
            public static IEnumerable<object[]> GetOnePessoasDataLoadController { get; } = new[]
            {
                new object[]
                {
                    "16",
                    new PersonDto
                    {
                        Pessoas = new PersonsInfoDto
                        {
                            Id_Pessoas = "613",
                            Nome = "TesteGetMethodController3",
                            Sobrenome = "TesteGetMethodController4",
                            Pessoas_Calc_Number = 72.7M,
                            DataHora = DateTime.Now
                        },
                    }
                }
            };

            public static IEnumerable<object[]> GetOnePessoasDataLoadControllerNotFound { get; } = new[]
            {
                new object[]
                {
                    "16",
                }
            };

            public static IEnumerable<object[]> GetMediaDataLoadController { get; } = new[]
            {
                new object[]
                {
                    56.9M,
                }
            };

            public static IEnumerable<object[]> CreatePessoasDataLoadController { get; } = new[]
            {
                new object[]
                {
                    new PersonDto
                    {
                        Pessoas = new PersonsInfoDto
                        {
                            Id_Pessoas = "333",
                            Nome = "TesteGetMethodController5",
                            Sobrenome = "TesteGetMethodController6",
                            Pessoas_Calc_Number = 33.3M,
                            DataHora = DateTime.Now
                        },
                    }
                }
            };

            public static IEnumerable<object[]> CreatePessoasDataLoadControllerBadRequest { get; } = new[]
            {
                new object[]
                {
                    new PersonDto
                    {
                    },
                    "Person already exists in database!",
                }
            };

            public static IEnumerable<object[]> UpdatePessoasDataLoadController { get; } = new[]
            {
                new object[]
                {
                    new PersonDto
                    {
                        Pessoas = new PersonsInfoDto
                        {
                            Id_Pessoas = "771",
                            Nome = "TestControllerMethod7",
                            Sobrenome = "TestControllerMethod8",
                            Pessoas_Calc_Number = 11.1M,
                            DataHora = DateTime.Now
                        },
                    }
                }
            };

            public static IEnumerable<object[]> DeletePessoasDataLoadController { get; } = new[]
            {
                new object[]
                {
                    "601",
                    new List<Persons>
                    {
                        new Persons
                        {
                            Id_Pessoas = "812",
                            Nome = "TestControllerDeletePessoas1",
                            Sobrenome = "TestControllerDeletePessoas2",
                            Pessoas_Calc_Number = 99.9M,
                            DataHora = DateTime.Now,
                            ComidaComprada = new List<Food>
                            {
                                new Food
                                {
                                    Id_Food = "151",
                                    Name_Food = "TestControllerDeleteFood1",
                                    Locale_Purcache_Food = "TestControllerDeleteFood2",
                                    Id_Pessoas_References = "9191",
                                    Price_Food = 555.2M
                                },
                            },
                        },
                    }
                }
            };

            public static IEnumerable<object[]> DeletePessoasDataLoadControllerNotFound { get; } = new[]
            {
                new object[]
                {
                    "601",
                    "Data not Found in request!",
                }
            };

        }
    }
}
