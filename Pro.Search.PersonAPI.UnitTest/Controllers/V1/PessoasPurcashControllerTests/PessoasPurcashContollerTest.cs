using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasPurcashControllerTests
{
    [TestClass]
    public sealed partial class PessoasPurcashContollerTest : TestBase
    {
        [DataTestMethod]
        [DynamicData(nameof(DataSources.PersonPurcashControllerDataLoad), typeof(DataSources))]
        public async Task PessoasPurcashedFoodReturnOk(
            string id_pessoa,
            PersonPurcashDto personPurcashDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<GetPersonPurcashFoodQuery>(x => x.Id_Pessoas == id_pessoa))
                .Returns(personPurcashDto);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PersonsPurcashedFood(id_pessoa)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<PersonPurcashDto>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.PersonPurcashControllerDataLoadNotFound), typeof(DataSources))]
        public async Task PessoasPurcashedFoodReturnNotFound(
            string id_pessoa)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<GetPersonPurcashFoodQuery>(x => x.Id_Pessoas == id_pessoa))
                .Returns((PersonPurcashDto)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PersonsPurcashedFood(id_pessoa)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<NotFoundResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.PatchPersonPurcashControllerDataLoad), typeof(DataSources))]
        public async Task PatchPessoasPurcashedFoodReturnOk(
            string id_pessoa,
            PersonPurcashDto personPurcashDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<SetPessoasPurcashCommand>(x => x.Id_Pessoa == id_pessoa))
                .Returns(personPurcashDto);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PersonsPurcashedFood(id_pessoa, personPurcashDto)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<PersonPurcashDto>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.PatchPersonPurcashControllerDataLoadNotFound), typeof(DataSources))]
        public async Task PatchPessoasPurcashedFoodReturnNotFound(
            string id_pessoa,
            PersonPurcashDto personPurcashDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<SetPessoasPurcashCommand>(x => x.Id_Pessoa == id_pessoa))
                .Returns((PersonPurcashDto)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PersonsPurcashedFood(id_pessoa, personPurcashDto)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<NotFoundResult>();
        }
    }
}
