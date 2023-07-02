using AutoMapper;
using FluentAssertions;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Mappers;
using Pro.Search.Infraestructure.Repositories;
using Pro.Search.PersonCommands;
using Pro.Search.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasTest
{
    [TestClass]
    public sealed partial class PessoasTest
    {
        [DataTestMethod]
        [DynamicData(nameof(DataSouces.GetAllPersonsDataSource), typeof(DataSouces))]
        public async Task HandleGetPessoas(
            List<Persons> persons)
        {
            // Arrange
            var request = new GetAllPersonQuery();

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindAllAsyncPerson(CancellationToken.None).Returns(persons);

            var handler = QueryHandler(repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().BeOfType(typeof(List<PersonsAllInfoDto>));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.MediaCalcDataSource), typeof(DataSouces))]
        public async Task HandleGetMediaPessoas(
            List<decimal> media)
        {
            // Arrange
            var request = new GetMediaPersonQuery();

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.CalcMediaPersonNumber(CancellationToken.None).Returns(media);
            
            var handler = QueryHandlerMediaPerson(repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().Be(12.045M);
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.GetOnePersonDataSource), typeof(DataSouces))]
        public async Task HandleGetOnePessoa(
            string id_pessoa,
            Persons persons)
        {
            // Arrange
            var request = new GetOnePersonQuery(id_pessoa);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindOneAsyncPerson(
                id_pessoa, CancellationToken.None)
                .Returns(persons);

            var handler = QueryOneHandler(repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.GetPersonPurcashFood), typeof(DataSouces))]
        public async Task HandleGetPersonPurcashFood(
            string id_pessoa,
            Persons persons)
        {
            // Arrange
            var request = new GetPersonPurcashFoodQuery(id_pessoa);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindPersonPurcashFood(
                id_pessoa, CancellationToken.None)
                .Returns(persons);

            var handler = QueryPersonFoodPurcash(repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.CreatePersonDataSource), typeof(DataSouces))]
        public async Task HandleCreatePersonCommand(
            PersonDto personDto,
            Persons persons)
        {
            // Prepare
            var request = new CreatePersonCommand(personDto);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindOneAsyncPerson(
                request.PersonDto.Pessoas.Id_Pessoas, 
                CancellationToken.None)
                .Returns(persons);
            
            var handler = CreatePersonCommandHandlerData(
                Substitute.For<ISystemReadDBContext>(), 
                repository);

            // Act
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);

            // Assert
            _ = result.Value
                .Should()
                .BeOfType<CreateOrUpdateResponses.Success>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.UpdatePersonDataSource), typeof(DataSouces))]
        public async Task HandleUpdatePersonCommand(
            PersonDto personDto,
            Persons persons)
        {
            // Arrange
            var request = new UpdatePersonCommand(personDto);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.FindOneAsyncPerson(
                request.PersonDto.Pessoas.Id_Pessoas, 
                CancellationToken.None)
                .Returns(persons);

            var handler = UpdatePersonCommandHandlerData(
                Substitute.For<ISystemReadDBContext>(), 
                repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Value
                .Should()
                .BeOfType<CreateOrUpdateResponses.Success>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.DeletePersonDataSource), typeof(DataSouces))]
        public async Task HandleDeletePersonCommandHandlerReturnBadRequest(
            string id_pessoa,
            List<Persons> pessoa)
        {
            // Arrange
            var request = new DeletePersonCommand(id_pessoa);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.SearchAllPersonToIdPerson(
                id_pessoa, CancellationToken.None)
                .Returns(pessoa);

            var handler = DeleteHandlePersonCommand(
                Substitute.For<ISystemReadDBContext>(), 
                repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);
            
            // Assert
            _ = result.AsT1
                .Should()
                .BeOfType<DeleteResponses.BadRequest>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSouces.DeletePersonBadRequestDataSource), typeof(DataSouces))]
        public async Task HandleDeletePersonCommandHandlerReturnSuccess(
            string id_pessoa,
            List<Persons> persons)
        {
            // Arrange
            var request = new DeletePersonCommand(id_pessoa);

            var repository = Substitute.For<IPersonsRepository>();
            _ = repository.SearchAllPersonToIdPerson(
                id_pessoa, 
                CancellationToken.None)
                .Returns(persons);

            var handler = DeleteHandlePersonCommand(
                Substitute.For<ISystemReadDBContext>(), 
                repository);

            // Act
            var result = await handler
                .Handle(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.AsT0
                .Should()
                .BeOfType<DeleteResponses.Success>();
        }

        [TestMethod]
        public async Task HandleGetOnePessoaExceptionNullArgument()
        {
            var handler = CreateHandlerExceptionGetOnePessoa();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HandleGetPersonPurcashFoodExceptionNullArgument()
        {
            var handler = CreateHandlerGetPersonPurcashFoodExceptionGetOnePessoa();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HandleUpdatePersonExceptionNullArgument()
        {
            var handler = CreateHandlerUpdatePersonExceptionGetOnePessoa();

            Func<Task> result = async () => _ = await handler.Handle(default, default).ConfigureAwait(false);

            _ = await result.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
        }

        private static GetAllPersonQueryHandler QueryHandler(IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllPersonQueryHandler(repository, mapper);
        }

        private static GetMediaPersonQueryHandler QueryHandlerMediaPerson(IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            return new GetMediaPersonQueryHandler(repository);
        }

        private static GetOnePersonQueryHandler QueryOneHandler(IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetOnePersonQueryHandler(repository, mapper);
        }

        private static GetPersonPurcashFoodQueryHandler QueryPersonFoodPurcash(IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetPersonPurcashFoodQueryHandler(repository, mapper);
        }

        private static CreatePersonCommandHandler CreatePersonCommandHandlerData(ISystemReadDBContext _context, IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new CreatePersonCommandHandler(
                Substitute.For<ISystemReadDBContext>(), 
                mapper, Substitute.For<IPersonsRepository>());
        }

        private static UpdatePersonCommandHandler UpdatePersonCommandHandlerData(ISystemReadDBContext _context, IPersonsRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new UpdatePersonCommandHandler(
                Substitute.For<ISystemReadDBContext>(), 
                repository ?? Substitute.For<IPersonsRepository>(), 
                mapper);
        }

        private static DeletePersonCommandHandler DeleteHandlePersonCommand(ISystemReadDBContext context, IPersonsRepository repository)
        {
            return new DeletePersonCommandHandler(
                context ?? Substitute.For<ISystemReadDBContext>(), 
                repository ?? Substitute.For<IPersonsRepository>());
        }

        private static GetPersonPurcashFoodQueryHandler CreateHandlerGetPersonPurcashFoodExceptionGetOnePessoa(
            IPersonsRepository repository = default, IMapper mapper = default)
        {
            return new GetPersonPurcashFoodQueryHandler(
                repository ?? Substitute.For<IPersonsRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        private static GetOnePersonQueryHandler CreateHandlerExceptionGetOnePessoa(IPersonsRepository repository = default, IMapper mapper = default)
        {
            return new GetOnePersonQueryHandler(
                repository ?? Substitute.For<IPersonsRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        private static UpdatePersonCommandHandler CreateHandlerUpdatePersonExceptionGetOnePessoa(
            ISystemReadDBContext context = default, IMapper mapper = default, IPersonsRepository repository = default)
        {
            return new UpdatePersonCommandHandler(
                context ?? Substitute.For<ISystemReadDBContext>(),
                repository ?? Substitute.For<IPersonsRepository>(), 
                mapper);
        }
    }
}
