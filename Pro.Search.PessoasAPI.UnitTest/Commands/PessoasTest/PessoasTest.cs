using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasTest
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

            var handler = QueryHandler(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(List<PessoasAllInfoDto>));
        }

        [TestMethod]
        public async Task HandleGetOnePessoa()
        {
            var id_pessoa = "0001";
            var request = new GetOnePersonQuery(id_pessoa);

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindOneAsyncPerson(id_pessoa, CancellationToken.None).Returns(
                    new Pessoas
                    {
                        Id_Pessoas = id_pessoa,
                    });

            var handler = QueryOneHandler(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should().NotBeNull();
            }
        }


        protected static GetAllPersonQueryHandler QueryHandler(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllPersonQueryHandler(repository, mapper);
        }

        protected static GetOnePersonQueryHandler QueryOneHandler(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetOnePersonQueryHandler(repository, mapper);
        }
    }
}
