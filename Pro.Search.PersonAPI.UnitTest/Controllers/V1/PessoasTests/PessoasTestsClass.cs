using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.PersonCommands;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasTests
{
    [TestClass]
    public sealed partial class PessoasTestsClass : TestBase
    {
        [DataTestMethod]
        [DynamicData(nameof(DataSources.GetPessoasDataLoadController), typeof(DataSources))]
        public async Task GetPessoasShouldReturnOk(
            List<PersonsAllInfoDto> personsAllInfoDtos)
        {
            // Arrage
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Any<GetAllPersonQuery>())
                .Returns(personsAllInfoDtos);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .ListPersons()
                .ConfigureAwait(false);
            
            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<PersonsAllInfoDto>>();
            
        }

        [TestMethod]
        public async Task GetPessoasShouldReturnNotFound()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Any<GetAllPersonQuery>())
                .Returns((List<PersonsAllInfoDto>)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller.ListPersons().ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<NotFoundResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.GetOnePessoasDataLoadController), typeof(DataSources))]
        public async Task GetOnePessoasShoudReturnOk(
            string id_pessoa,
            PersonDto personDto)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<GetOnePersonQuery>(x => x.Id_Pessoas == id_pessoa))
                .Returns(personDto);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .SearchOnePerson(id_pessoa)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<PersonDto>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.GetOnePessoasDataLoadControllerNotFound), typeof(DataSources))]
        public async Task GetOnePessoasShouldReturnNotFound(
            string id_pessoa)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<GetOnePersonQuery>(x => x.Id_Pessoas == id_pessoa))
                .Returns((PersonDto)default);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .SearchOnePerson(id_pessoa)
                .ConfigureAwait(false);
            
            // Assert
            _ = response.Should().BeOfType<NotFoundResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.GetMediaDataLoadController), typeof(DataSources))]
        public async Task GetMediaPessoasShouldReturnOk(
            decimal media)
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Any<GetMediaPersonQuery>())
                .Returns(media);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .CalcMedia()
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<OkObjectResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.CreatePessoasDataLoadController), typeof(DataSources))]
        public async Task CreatePessoasShouldReturnOk(
            PersonDto personDto)
        {
            // Arrange
            var output = new CreateOrUpdateResponses.Success(personDto);
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<CreatePersonCommand>(x => x.PersonDto == personDto))
                .Returns(output);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PostPersons(personDto)
                .ConfigureAwait(false);
            
            // Assert
            _ = response.Should().BeOfType<OkObjectResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.CreatePessoasDataLoadControllerBadRequest), typeof(DataSources))]
        public async Task CreatePessoasShouldReturnBadRequest(
            PersonDto personDto,
            string message)
        {
            // Arrange
            var output = new CreateOrUpdateResponses.BadRequest(message);
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<CreatePersonCommand>(x => x.PersonDto == personDto))
                .Returns(output);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .PostPersons(personDto)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.UpdatePessoasDataLoadController), typeof(DataSources))]
        public async Task UpdatePessoasShouldReturnOk(
            PersonDto personDto)
        {
            // Arrange
            var output = new CreateOrUpdateResponses.Success(personDto);
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<UpdatePersonCommand>(x => x.PersonDto == personDto))
                .Returns(output);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .UpdatePerson(personDto)
                .ConfigureAwait(false);
            
            // Assert
            _ = response.Should().BeOfType<OkObjectResult>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.DeletePessoasDataLoadController), typeof(DataSources))]
        public async Task DeletePessoasShouldReturnOK(
            string id_pessoa,
            List<Persons> persons)
        {
            // Arrange
            var output = new DeleteResponses.Success(persons);
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<DeletePersonCommand>(x => x.Id_Pessoas == id_pessoa))
                .Returns(output);
            
            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .DeletePerson(id_pessoa)
                .ConfigureAwait(false);

            // Assert
            _ = response
                .Should()
                .BeOfType<OkObjectResult>()
                .Which.Value.Should()
                .BeOfType<List<Persons>>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSources.DeletePessoasDataLoadControllerNotFound), typeof(DataSources))]
        public async Task DeletePessoasShouldReturnNotFound(
            string id_pessoa,
            string message)
        {
            // Arrange
            var output = new DeleteResponses.BadRequest(message);
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(
                Arg.Is<DeletePersonCommand>(x => x.Id_Pessoas == id_pessoa))
                .Returns(output);

            var controller = CreateController(mediator);

            // Act
            var response = await controller
                .DeletePerson(id_pessoa)
                .ConfigureAwait(false);

            // Assert
            _ = response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
