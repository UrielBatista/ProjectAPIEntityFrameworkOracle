using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.FoodsTest
{
    [TestClass]
    public class FoodTest
    {
        [TestMethod]
        public async Task HandleGetAllFoodQuery()
        {
            // Prepare
            var request = new GetAllFoodQuery();

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindAllAsyncFood(CancellationToken.None).Returns(
                new List<Food>
                {
                    new Food
                    {
                        Id_Food = "159753",
                    },
                });

            // Assert
            var handler = QueryHandlerAllFood(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(List<FoodAllInfoDto>));
        }

        protected static GetAllFoodQueryHandler QueryHandlerAllFood(IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllFoodQueryHandler(repository, mapper);
        }
    }
}
