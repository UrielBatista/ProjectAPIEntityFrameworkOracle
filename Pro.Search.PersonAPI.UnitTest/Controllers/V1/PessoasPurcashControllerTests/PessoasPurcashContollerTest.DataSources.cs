using Microsoft.VisualStudio.TestPlatform.Common;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasPurcashControllerTests
{
    public sealed partial class PessoasPurcashContollerTest
    {
        private static class DataSources
        {
            public static IEnumerable<object[]> PersonPurcashControllerDataLoad { get; } = new[]
            {
                new object[]
                {
                    "2242",
                    new PersonPurcashDto
                    {
                        Pessoas = new PersonsAllInfoDto
                        {
                            Id_Pessoas = "412",
                            Nome = "TestingfMethodControllerPersonPurcash1",
                            Sobrenome = "TestingfMethodControllerPersonPurcash2",
                            Pessoas_Calc_Number = 12.7M,
                            DataHora = DateTime.Now
                        },
                        Food = new List<FoodAllInfoDto>
                        {
                            new FoodAllInfoDto
                            {
                                Id_Food = "5125",
                                Nome = "TestingfMethodControllerPersonPurcash3",
                                LocalDeVenda = "TestingfMethodControllerPersonPurcash4",
                                ReferenciaIdPessoa = "TestingfMethodControllerPersonPurcash5",
                                PrecoComida = 22.78M
                            },
                        },
                    }
                }
            };

            public static IEnumerable<object[]> PersonPurcashControllerDataLoadNotFound { get; } = new[]
            {
                new object[]
                {
                    "2242",
                }
            };

            public static IEnumerable<object[]> PatchPersonPurcashControllerDataLoad { get; } = new[]
            {
                new object[]
                {
                    "2242",
                    new PersonPurcashDto
                    {
                        Pessoas = new PersonsAllInfoDto
                        {
                            Id_Pessoas = "412",
                            Nome = "TestingfMethodControllerPersonPurcash1",
                            Sobrenome = "TestingfMethodControllerPersonPurcash2",
                            Pessoas_Calc_Number = 12.7M,
                            DataHora = DateTime.Now
                        },
                        Food = new List<FoodAllInfoDto>
                        {
                            new FoodAllInfoDto
                            {
                                Id_Food = "5125",
                                Nome = "TestingfMethodControllerPersonPurcash3",
                                LocalDeVenda = "TestingfMethodControllerPersonPurcash4",
                                ReferenciaIdPessoa = "TestingfMethodControllerPersonPurcash5",
                                PrecoComida = 22.78M
                            },
                        },
                    }
                }
            };

            public static IEnumerable<object[]> PatchPersonPurcashControllerDataLoadNotFound { get; } = new[]
            {
                new object[]
                {
                    "8819",
                    new PersonPurcashDto
                    {
                    }
                }
            };
        }
    }
}
