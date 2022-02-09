using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.FoodsTests
{
    [TestClass]
    public sealed class FoodsTestsClass : TestBase
    {
        [TestMethod]
        public async Task GetFoodsShouldReturnOk()
        {
            // Prepare
            var results = new List<FoodAllInfoDto>
            {
                new FoodAllInfoDto
                {
                    Id_Food = "51092",
                    Nome = "TestGetFoodMethodController1",
                    LocalDeVenda = "TestGetFoodMethodController2",
                    ReferenciaIdPessoa = "19747",
                    PrecoComida = 88.8M
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<GetAllFoodQuery>()).Returns(results);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.ListarComida().ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<FoodAllInfoDto>>();
            }
        }

        [TestMethod]
        public async Task GetFoodsShouldNotFound()
        {
            // Prepare
            var results = new List<FoodAllInfoDto>
            {
                default
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<GetAllFoodQuery>()).Returns(((List<FoodAllInfoDto>)default));

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.ListarComida().ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }

        [TestMethod]
        public async Task CreateFoodShouldReturnOk()
        {
            // Prepare
            var result = new FoodAllInfoDto
            {
                Id_Food = "8484",
                Nome = "TestPostMethodCreateFood1",
                LocalDeVenda = "TestPostMethodCreateFood2",
                ReferenciaIdPessoa = "1212",
                PrecoComida = 44.4M
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<CreateFoodInMemoryCommand>(p => p.FoodAllInfoDto == result)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.InserirComida(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<FoodAllInfoDto>();
            }
        }

        [TestMethod]
        public async Task CreateFoodShouldBadRequest()
        {
            // Prepare
            var result = new FoodAllInfoDto
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<CreateFoodInMemoryCommand>(p => p.FoodAllInfoDto == result)).Returns((FoodAllInfoDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.InserirComida(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<BadRequestResult>();
            }
        }

        [TestMethod]
        public async Task UpdateFoodShouldReturnOk()
        {
            // Prepare
            var result = new FoodAllInfoDto
            {
                Id_Food = "3390",
                Nome = "TestUpdateMethodCreateFood3",
                LocalDeVenda = "TestUpdateMethodCreateFood4",
                ReferenciaIdPessoa = "6661",
                PrecoComida = 22.2M
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<UpdateFoodCommand>(p => p.FoodAllInfoDto == result)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.AtualizarComida(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<FoodAllInfoDto>();
            }
        }

        [TestMethod]
        public async Task UpdateFoodShouldReturnBadRequest()
        {
            // Prepare
            var result = new FoodAllInfoDto
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<UpdateFoodCommand>(p => p.FoodAllInfoDto == result)).Returns((FoodAllInfoDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.AtualizarComida(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<BadRequestResult>();
            }
        }

        [TestMethod]
        public async Task DeleteFoodShouldReturnOk()
        {
            // Prepare
            var id_food = "7754";

            var result = new List<Food>
            {
                new Food
                {
                    Id_Food = "4489",
                    Name_Food = "TestDeleteMethodCreateFood4",
                    Locale_Purcache_Food = "TestDeleteMethodCreateFood5",
                    Id_Pessoas_References = "0019",
                    Price_Food = 52.38M
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<DeleteFoodCommand>(p => p.Id_Food == id_food)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.DeletearComida(id_food).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<Food>>();
            }
        }

        [TestMethod]
        public async Task DeleteFoodShouldReturnNotFound()
        {
            var id_food = "7754";

            var result = new List<Food>
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<DeleteFoodCommand>(p => p.Id_Food == id_food)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.DeletearComida(id_food).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
