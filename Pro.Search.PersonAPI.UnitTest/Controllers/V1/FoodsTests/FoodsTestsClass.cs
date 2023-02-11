using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.FoodsTests
{
    [TestClass]
    public sealed partial class FoodsTestsClass : TestBase
    {
        [DataTestMethod]
        [DynamicData(nameof(DataSources.CreateFoodDataLoadController), typeof(DataSources))]
        public async Task CreateFoodShouldReturnOk(
            FoodAllInfoDto foodAllInfoDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<CreateFoodCommand>(p => p.FoodAllInfoDto == foodAllInfoDto))
                .Returns(foodAllInfoDto);
            
            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PostFood(foodAllInfoDto)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<FoodAllInfoDto>();
            
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.CreateFoodDataLoadControllerBadRequest), typeof(DataSources))]
        public async Task CreateFoodShouldBadRequest(
            FoodAllInfoDto foodAllInfoDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<CreateFoodInMemoryCommand>(p => p.FoodAllInfoDto == foodAllInfoDto))
                .Returns((FoodAllInfoDto)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PostFood(foodAllInfoDto)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<BadRequestResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.UpdateFoodDataLoadController), typeof(DataSources))]
        public async Task UpdateFoodShouldReturnOk(
            FoodAllInfoDto foodAllInfoDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<UpdateFoodCommand>(p => p.FoodAllInfoDto == foodAllInfoDto))
                .Returns(foodAllInfoDto);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .UpdateFood(foodAllInfoDto)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<FoodAllInfoDto>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.UpdateFoodDataLoadControllerBadRequest), typeof(DataSources))]
        public async Task UpdateFoodShouldReturnBadRequest(
            FoodAllInfoDto foodAllInfoDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<UpdateFoodCommand>(p => p.FoodAllInfoDto == foodAllInfoDto))
                .Returns((FoodAllInfoDto)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .UpdateFood(foodAllInfoDto)
                .ConfigureAwait(false);
            
            // Assert
            _ = response.Should().BeOfType<NoContentResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.DeleteDataLoadController), typeof(DataSources))]
        public async Task DeleteFoodShouldReturnOk(
            string id_food,
            List<Food> food)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<DeleteFoodCommand>(p => p.Id_Food == id_food))
                .Returns(food);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .DeleteFood(id_food)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<Food>>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.DeleteDataLoadControllerNotFound), typeof(DataSources))]
        public async Task DeleteFoodShouldReturnNotFound(
            string id_food,
            List<Food> foods)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<DeleteFoodCommand>(p => p.Id_Food == id_food))
                .Returns(foods);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .DeleteFood(id_food)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<NoContentResult>();
        }
    }
}
