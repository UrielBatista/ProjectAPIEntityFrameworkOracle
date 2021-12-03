using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
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

        [TestMethod]
        public async Task HandleCreateFoodCommand()
        {
            var request = new CreateFoodCommand(
                new FoodAllInfoDto
                {
                    Id_Food = "654321",
                    Nome = "TestandoCreate2",
                    LocalDeVenda = "TestandoLocalCreate2",
                    ReferenciaIdPessoa = "159753",
                    PrecoComida = 66.6M
                });

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindOneAsyncFood(request.FoodAllInfoDto.Id_Food, CancellationToken.None).Returns(
                new Food() 
                {
                    Id_Food = "654321",
                    Name_Food = "TestandoCreate2",
                    Locale_Purcache_Food = "TestandoLocalCreate2",
                    Id_Pessoas_References = "159753",
                    Price_Food = 66.6M
                });

            var handler = CreateFoodCommandHandlerData(Substitute.For<IContextDB>() ,repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(FoodAllInfoDto));
        }

        [TestMethod]
        public async Task HandleCreateFoodCommandThrowExceptionArgumentIsNull()
        {
            var handler = CreateHandlerFoodCommandThrowExceptionArgumentIsNull();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);

        }

        protected static GetAllFoodQueryHandler QueryHandlerAllFood(IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllFoodQueryHandler(repository, mapper);
        }

        protected static CreateFoodCommandHandler CreateFoodCommandHandlerData(IContextDB _context, IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new CreateFoodCommandHandler(Substitute.For<IContextDB>(), mapper, repository ?? Substitute.For<IFoodRepository>());
        }

        private static CreateFoodCommandHandler CreateHandlerFoodCommandThrowExceptionArgumentIsNull(
            IContextDB _context = default, IMapper mapper = default, IFoodRepository repository = default)
        {
            return new CreateFoodCommandHandler(_context ?? Substitute.For<IContextDB>(), mapper ?? Substitute.For<IMapper>(), repository ?? Substitute.For<IFoodRepository>());
        }
    }
}
