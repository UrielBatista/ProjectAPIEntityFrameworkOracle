using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasPurcashTest
{
    [TestClass]
    public class PersonPurcashTestUnit
    {
        [TestMethod]
        public async Task HandleGetPersonPurcashFoodAsyncReturnOk()
        {
            // Prepare
            var id_pessoas = "8410";
            

            var request = new GetPersonPurcashFoodQuery(id_pessoas);

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindPersonPurcashFood(id_pessoas, CancellationToken.None).Returns(
                new Pessoas 
                { 
                    Id_Pessoas = id_pessoas 
                });

            // Assert
            var handler = GetPersonPurcashFoodQueryHandlerHandlerData(repository);
            var response = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = response.Should().BeOfType(typeof(PersonPurcashDto));
        }

        protected static GetPersonPurcashFoodQueryHandler GetPersonPurcashFoodQueryHandlerHandlerData(IPessoasRepository pessoasRepository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetPersonPurcashFoodQueryHandler(
                pessoasRepository ?? Substitute.For<IPessoasRepository>(), 
                mapper);
        }
    }
}
