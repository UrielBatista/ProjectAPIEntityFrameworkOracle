using Microsoft.VisualStudio.TestPlatform.Common;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.FoodsTest
{
    public sealed partial class FoodTest
    {
        private static class DataSources
        {
            public static IEnumerable<object[]> GetListAllFoods { get; } = new[]
            {
                new object[]
                {
                    new PageAndFilteredRequestParams
                    {
                        PageNumber = 1,
                        PageSize = 10,
                        flagsValue = true,
                    },
                    new List<Food>
                    {
                        new Food
                        {
                            Id_Food = "159753",
                        },
                    },
                }
            };

            public static IEnumerable<object[]> PostFoodDatas { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "654321",
                        Nome = "TestandoCreate2",
                        LocalDeVenda = "TestandoLocalCreate2",
                        ReferenciaIdPessoa = "159753",
                        PrecoComida = 66.6M
                    },
                    new Food
                    {
                        Id_Food = "654321",
                        Name_Food = "TestandoCreate2",
                        Locale_Purcache_Food = "TestandoLocalCreate2",
                        Id_Pessoas_References = "159753",
                        Price_Food = 66.6M
                    },
                }
            };

            public static IEnumerable<object[]> UpdateFoodDatas { get; } = new[]
            {
                new object[]
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "654321",
                        Nome = "TestandoCreate2",
                        LocalDeVenda = "TestandoLocalCreate2",
                        ReferenciaIdPessoa = "159753",
                        PrecoComida = 66.6M
                    },
                    new Food
                    {
                        Id_Food = "654321",
                        Name_Food = "TestandoCreate2",
                        Locale_Purcache_Food = "TestandoLocalCreate2",
                        Id_Pessoas_References = "159753",
                        Price_Food = 66.6M
                    },
                }
            };

            public static IEnumerable<object[]> DeleteFoodDataParam { get; } = new[]
            {
                new object[]
                {
                    "7858",
                    new List<Food>
                    {
                        new Food
                        {
                            Id_Food = "88585",
                            Name_Food = "TesteHandleFood1",
                            Locale_Purcache_Food = "TestandoLocalHandle1",
                            Id_Pessoas_References = "04682",
                            Price_Food = 44.6M
                        }
                    },
                }
            };
        }
    }
}
