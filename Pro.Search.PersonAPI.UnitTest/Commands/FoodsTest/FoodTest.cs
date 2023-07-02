using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonDomains.PersonEngine.Commons;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.FoodsTest
{
    [TestClass]
    public sealed partial class FoodTest
    {
        
        [DataTestMethod]
        [DynamicData(nameof(DataSources.GetListAllFoods), typeof(DataSources))]
        public async Task HandleGetAllFoodQuery(
            PageAndFilteredRequestParams requestDataPageFilteredRequestParams,
            IEnumerable<Food> returnListFoodEntity)
        {
            // Arrange
            var request = new GetAllFoodQuery(requestDataPageFilteredRequestParams);

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindAllAsyncFood(
                page: requestDataPageFilteredRequestParams.PageNumber, 
                pageSize: requestDataPageFilteredRequestParams.PageSize,
                requestDataPageFilteredRequestParams.flagsValue, 
                CancellationToken.None).Returns(returnListFoodEntity);

            // Act
            var handler = QueryHandlerAllFood(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _ = result.Should().BeOfType(typeof(FoodResponse));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.PostFoodDatas), typeof(DataSources))]
        public async Task HandleCreateFoodCommand(
            FoodAllInfoDto requestFoodAllInfoDto,
            Food returnRepositoryEntity)
        {
            // Arrange
            var request = new CreateFoodCommand(requestFoodAllInfoDto);

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindOneAsyncFood(
                request.FoodAllInfoDto.Id_Food, 
                CancellationToken.None)
                .Returns(returnRepositoryEntity);

            var handler = CreateFoodCommandHandlerData(
                Substitute.For<ISystemReadDBContext>(), 
                repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().BeOfType(typeof(FoodAllInfoDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.UpdateFoodDatas), typeof(DataSources))]
        public async Task HandleUpdateFoodCommand(
            FoodAllInfoDto requestFoodAllInfoDto,
            Food returnRepositoryEntity)
        {
            // Arrange
            var request = new UpdateFoodCommand(requestFoodAllInfoDto);

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindOneAsyncFood(
                request.FoodAllInfoDto.Id_Food, 
                CancellationToken.None)
                .Returns(returnRepositoryEntity);

            //Act
            var handler = UpdateFoodCommandHandlerData(
                Substitute.For<ISystemReadDBContext>(), repository);
            
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().BeOfType(typeof(FoodAllInfoDto));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.DeleteFoodDataParam), typeof(DataSources))]
        public async Task HandleDeleteFoodCommandHandler(
            string idFood,
            IEnumerable<Food> returnFoodOfRepository)
        {
            // Arrange
            var request = new DeleteFoodCommand(idFood);

            var repository = Substitute.For<IFoodRepository>();
            _ = repository.FindListFoodReferenceToIDFood(
                idFood, 
                CancellationToken.None)
                .Returns(returnFoodOfRepository);

            var handler = DeleteFoodCommandHandlerData(
                Substitute.For<ISystemReadDBContext>(), repository);

            //Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull();
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

        private static GetAllFoodQueryHandler QueryHandlerAllFood(IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllFoodQueryHandler(repository, mapper);
        }

        private static CreateFoodCommandHandler CreateFoodCommandHandlerData(ISystemReadDBContext _context, IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new CreateFoodCommandHandler(
                Substitute.For<ISystemReadDBContext>(), 
                mapper, 
                repository ?? Substitute.For<IFoodRepository>());
        }

        private static UpdateFoodCommandHandler UpdateFoodCommandHandlerData(ISystemReadDBContext _context, IFoodRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<FoodProfile>());
            var mapper = new Mapper(mapperConf);
            return new UpdateFoodCommandHandler(
                Substitute.For<ISystemReadDBContext>(),
                repository ?? Substitute.For<IFoodRepository>(),
                mapper);
        }

        private static CreateFoodCommandHandler CreateHandlerFoodCommandThrowExceptionArgumentIsNull(
            ISystemReadDBContext _context = default, IMapper mapper = default, IFoodRepository repository = default)
        {
            return new CreateFoodCommandHandler(
                _context ?? Substitute.For<ISystemReadDBContext>(), 
                mapper ?? Substitute.For<IMapper>(), 
                repository ?? Substitute.For<IFoodRepository>());
        }

        private static UpdateFoodCommandHandler UpdateHandlerFoodCommandThrowExceptionArgumentIsNull(
            ISystemReadDBContext _context = default, IMapper mapper = default, IFoodRepository repository = default)
        {
            return new UpdateFoodCommandHandler(
                _context ?? Substitute.For<ISystemReadDBContext>(),
                repository ?? Substitute.For<IFoodRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        private static DeleteFoodCommandHandler DeleteFoodCommandHandlerData(ISystemReadDBContext _context, IFoodRepository foodRepository)
        {
            return new DeleteFoodCommandHandler(
                _context ?? Substitute.For<ISystemReadDBContext>(), 
                foodRepository ?? Substitute.For<IFoodRepository>());
        }

        private static DeleteFoodCommandHandler DeleteHandlerFoodCommandThrowExceptionArgumentIsNull(
            ISystemReadDBContext _context = default, IFoodRepository foodRepository = default)
        {
            return new DeleteFoodCommandHandler(
                _context ?? Substitute.For<ISystemReadDBContext>(), 
                foodRepository ?? Substitute.For<IFoodRepository>());
        }
    }
}
