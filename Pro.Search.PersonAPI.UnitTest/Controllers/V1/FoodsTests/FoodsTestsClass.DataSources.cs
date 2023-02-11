using Microsoft.VisualStudio.TestPlatform.Common;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.FoodsTests
{
    public sealed partial class FoodsTestsClass
    {
        private static class DataSources
        {
            public static IEnumerable<object[]> CreateFoodDataLoadController { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "8484",
                        Nome = "TestPostMethodCreateFood1",
                        LocalDeVenda = "TestPostMethodCreateFood2",
                        ReferenciaIdPessoa = "1212",
                        PrecoComida = 44.4M
                    },
                }
            };

            public static IEnumerable<object[]> CreateFoodDataLoadControllerBadRequest { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                    },
                }
            };

            public static IEnumerable<object[]> UpdateFoodDataLoadController { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "3390",
                        Nome = "TestUpdateMethodCreateFood3",
                        LocalDeVenda = "TestUpdateMethodCreateFood4",
                        ReferenciaIdPessoa = "6661",
                        PrecoComida = 22.2M
                    },
                }
            };

            public static IEnumerable<object[]> UpdateFoodDataLoadControllerBadRequest { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                    },
                }
            };

            public static IEnumerable<object[]> DeleteDataLoadController { get; } = new[]
            {
                new object[]
                {
                    "7754",
                    new List<Food>
                    {
                        new Food
                        {
                            Id_Food = "4489",
                            Name_Food = "TestDeleteMethodCreateFood4",
                            Locale_Purcache_Food = "TestDeleteMethodCreateFood5",
                            Id_Pessoas_References = "0019",
                            Price_Food = 52.38M
                        }
                    },
                }
            };

            public static IEnumerable<object[]> DeleteDataLoadControllerNotFound { get; } = new[]
            {
                new object[]
                {
                    "7754",
                    new List<Food>
                    {
                    },
                }
            };
        }
    }
}
