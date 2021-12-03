using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
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
            // Prepare
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

            // Assert
            var handler = CreateFoodCommandHandlerData(Substitute.For<IContextDB>() ,repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(FoodAllInfoDto));
        }

        [TestMethod]
        public async Task HandleUpdateFoodCommand()
        {
            // Prepare
            var request = new UpdateFoodCommand(
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

            // Assert
            var handler = UpdateFoodCommandHandlerData(Substitute.For<IContextDB>(), repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(FoodAllInfoDto));
        }

        [TestMethod]
        public async Task HandleDeleteFoodCommandHandler()
        {
            // Prepare
            var id_food = "7858";

            var food = new Food
            {
                Id_Food = "88585",
                Name_Food = "TesteHandleFood1",
                Locale_Purcache_Food = "TestandoLocalHandle1",
                Id_Pessoas_References = "04682",
                Price_Food = 44.6M
            };

            var request = new DeleteFoodCommand(id_food);

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindListFoodReferenceToIDFood(id_food, CancellationToken.None).Returns(
                new List<Food>
                    {
                        food
                    }
                );

            // Assert
            var handler = DeleteFoodCommandHandlerData(Substitute.For<IContextDB>(), repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should().NotBeNull();
            }
        }

        [TestMethod]
        public async Task HandleCreateFoodCommandThrowExceptionArgumentIsNull()
        {
            var handler = CreateHandlerFoodCommandThrowExceptionArgumentIsNull();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HandleUpdateFoodCommandThrowExceptionArgumentIsNull()
        {
            var handler = UpdateHandlerFoodCommandThrowExceptionArgumentIsNull();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HandleDeleteFoodCommandThrowExceptionArgumentIsNull()
        {
            var handler = DeleteHandlerFoodCommandThrowExceptionArgumentIsNull();

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
            return new CreateFoodCommandHandler(
                Substitute.For<IContextDB>(), 
                mapper, 
                repository ?? Substitute.For<IFoodRepository>());
        }

        protected static UpdateFoodCommandHandler UpdateFoodCommandHandlerData(IContextDB _context, IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new UpdateFoodCommandHandler(
                Substitute.For<IContextDB>(),
                repository ?? Substitute.For<IFoodRepository>(),
                mapper);
        }

        private static CreateFoodCommandHandler CreateHandlerFoodCommandThrowExceptionArgumentIsNull(
            IContextDB _context = default, IMapper mapper = default, IFoodRepository repository = default)
        {
            return new CreateFoodCommandHandler(
                _context ?? Substitute.For<IContextDB>(), 
                mapper ?? Substitute.For<IMapper>(), 
                repository ?? Substitute.For<IFoodRepository>());
        }

        private static UpdateFoodCommandHandler UpdateHandlerFoodCommandThrowExceptionArgumentIsNull(
            IContextDB _context = default, IMapper mapper = default, IFoodRepository repository = default)
        {
            return new UpdateFoodCommandHandler(
                _context ?? Substitute.For<IContextDB>(),
                repository ?? Substitute.For<IFoodRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        protected static DeleteFoodCommandHandler DeleteFoodCommandHandlerData(IContextDB _context, IFoodRepository foodRepository)
        {
            return new DeleteFoodCommandHandler(
                _context ?? Substitute.For<IContextDB>(), 
                foodRepository ?? Substitute.For<IFoodRepository>());
        }

        private static DeleteFoodCommandHandler DeleteHandlerFoodCommandThrowExceptionArgumentIsNull(
            IContextDB _context = default, IFoodRepository foodRepository = default)
        {
            return new DeleteFoodCommandHandler(
                _context ?? Substitute.For<IContextDB>(), 
                foodRepository ?? Substitute.For<IFoodRepository>());
        }
    }
}
