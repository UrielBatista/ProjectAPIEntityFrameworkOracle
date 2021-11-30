using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonCommands.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Pessoas.UnitTests.Commands.PessoasTest
{
    [TestClass]
    public class PessoasTest
    {
        [TestMethod]
        public async Task HandleGetPessoas()
        {
            var request = new GetAllPersonQuery();

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindAllAsyncPerson(CancellationToken.None).Returns(
                    new List<Pessoas>
                    {
                        new Pessoas
                        {
                            Id_Pessoas = "1234",
                        },
                    });
        }
    }
}
