using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasPurcashTest
{
    public sealed partial class PersonPurcashTestUnit
    {
        private static class DataSouces
        {
            public static IEnumerable<object[]> GetPersonPurcashWithFood { get; } = new[]
            {
                new object[]
                {
                    "8410",
                    new Persons
                    {
                        Id_Pessoas = "8410",
                    },
                }
            };
        }
    }
}
