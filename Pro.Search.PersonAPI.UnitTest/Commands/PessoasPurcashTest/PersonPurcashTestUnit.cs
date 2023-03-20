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
    public sealed partial class PersonPurcashTestUnit
    {
        [DataTestMethod]
        [DynamicData(nameof(DataSouces.GetPersonPurcashWithFood), typeof(DataSouces))]
        public async Task HandleGetPersonPurcashFoodAsyncReturnOk(
            string id_pessoas,
            Persons persons)
        {
            // Arrange
            var request = new GetPersonPurcashFoodQuery(id_pessoas);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindPersonPurcashFood(
                id_pessoas, CancellationToken.None)
                .Returns(persons);

            var handler = GetPersonPurcashFoodQueryHandlerHandlerData(repository);

            // Act
            var response = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType(typeof(PersonPurcashDto));
        }

        private static GetPersonPurcashFoodQueryHandler GetPersonPurcashFoodQueryHandlerHandlerData(IPersonsRepository pessoasRepository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetPersonPurcashFoodQueryHandler(
                pessoasRepository ?? Substitute.For<IPersonsRepository>(), 
                mapper);
        }
    }
}
