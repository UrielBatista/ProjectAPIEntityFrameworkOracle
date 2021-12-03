using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Commands.PessoasTest
{
    [TestClass]
    public class PessoasTest
    {
        [TestMethod]
        public async Task HandleGetPessoas()
        {
            // Prepare
            var request = new GetAllPersonQuery();

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindAllAsyncPerson(CancellationToken.None).Returns(
                    new List<Pessoas>
                    {
                        new Pessoas
                        {
                            Id_Pessoas = "1234",
                        },
                    });

            // Assert
            var handler = QueryHandler(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(List<PessoasAllInfoDto>));
        }

        [TestMethod]
        public async Task HandleGetMediaPessoas()
        {
            // Prepare
            var request = new GetMediaPersonQuery();

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindAllAsyncPerson(CancellationToken.None).Returns(
                new List<Pessoas>
                {
                    new Pessoas
                    {
                        Id_Pessoas = "519",
                    },
                });

            // Assert
            var handler = QueryHandlerMediaPerson(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should();
            }
        }

        [TestMethod]
        public async Task HandleGetOnePessoa()
        {
            // Prepare
            var id_pessoa = "0001";
            var request = new GetOnePersonQuery(id_pessoa);

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindOneAsyncPerson(id_pessoa, CancellationToken.None).Returns(
                    new Pessoas
                    {
                        Id_Pessoas = id_pessoa,
                    });

            // Assert
            var handler = QueryOneHandler(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should().NotBeNull();
            }
        }

        [TestMethod]
        public async Task HandleGetPersonPurcashFood()
        {
            // Prepare
            var id_pessoa = "5757";
            var request = new GetPersonPurcashFoodQuery(id_pessoa);

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindPersonPurcashFood(id_pessoa, CancellationToken.None).Returns(
                    new Pessoas
                    {
                        Id_Pessoas = id_pessoa,
                    });

            // Assert
            var handler = QueryPersonFoodPurcash(repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should().NotBeNull();
            }
        }
        
        [TestMethod]
        public async Task HandleCreatePersonCommand()
        {
            // Prepare
            var request = new CreatePersonCommand(
                new PersonDto
                {
                    Pessoas = new PessoasInfoDto
                    {
                        Id_Pessoas = "987456",
                        Nome = "CreateTestePerson",
                        Sobrenome = "CreateTesteSobrenomePerson",
                        Pessoas_Calc_Number = 11.2M,
                        DataHora = DateTime.Now
                    }
                });

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, CancellationToken.None).Returns(
                new Pessoas()
                {
                    Id_Pessoas = "987456",
                    Nome = "CreateTestePerson",
                    Sobrenome = "CreateTesteSobrenomePerson",
                    Pessoas_Calc_Number = 11.2M,
                    DataHora = DateTime.Now
                });

            // Assert
            var handler = CreatePersonCommandHandlerData(Substitute.For<IContextDB>(), repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(PersonDto));
        }

        [TestMethod]
        public async Task HandleUpdatePersonCommand()
        {
            // Prepare
            var request = new UpdatePersonCommand(
                new PersonDto
                {
                    Pessoas = new PessoasInfoDto
                    {
                        Id_Pessoas = "25752",
                        Nome = "UpdateTestePerson",
                        Sobrenome = "UpdateTesteSobrenomePerson",
                        Pessoas_Calc_Number = 12.6M,
                        DataHora = DateTime.Now
                    }
                });

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.FindOneAsyncPerson(request.PersonDto.Pessoas.Id_Pessoas, CancellationToken.None).Returns(
                new Pessoas()
                {
                    Id_Pessoas = "987456",
                    Nome = "CreateTestePerson",
                    Sobrenome = "CreateTesteSobrenomePerson",
                    Pessoas_Calc_Number = 11.2M,
                    DataHora = DateTime.Now
                });

            // Assert
            var handler = UpdatePersonCommandHandlerData(Substitute.For<IContextDB>(), repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            _ = result.Should().BeOfType(typeof(PersonDto));
        }

        [TestMethod]
        public async Task HandleDeletePersonCommandHandler()
        {
            // Prepare
            var id_pessoa = "6548";

            var pessoa = new Pessoas
            {
                Id_Pessoas = "04682",
                Nome = "TesteHandle1",
                Sobrenome = "TesteHandleSobrenome1",
                Pessoas_Calc_Number = 45.2M,
                DataHora = DateTime.Now,
                ComidaComprada = new List<Food>
                {
                    new Food
                    {
                        Id_Food = "88585",
                        Name_Food = "TesteHandleFood1",
                        Locale_Purcache_Food = "TestandoLocalHandle1",
                        Id_Pessoas_References = "04682",
                         Price_Food = 44.6M
                    }
                }
            };

            var request = new DeletePersonCommand(id_pessoa);

            var repository = Substitute.For<IPessoasRepository>();
            _ = repository.SearchAllPersonToIdPerson(id_pessoa, CancellationToken.None).Returns(
                new List<Pessoas>
                    {
                        new Pessoas()
                    }
                );

            // Assert
            var handler = DeleteHandlePersonCommand(Substitute.For<IContextDB>(), repository);
            var result = await handler.Handle(request, CancellationToken.None).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = result.Should().NotBeNull();
            }
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
        public async Task HandleCreatePersonExceptionNullArgument()
        {
            var handler = CreateHandlerSetPersonExceptionGetOnePessoa();

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

        protected static GetAllPersonQueryHandler QueryHandler(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetAllPersonQueryHandler(repository, mapper);
        }

        protected static GetMediaPersonQueryHandler QueryHandlerMediaPerson(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            return new GetMediaPersonQueryHandler(repository);
        }

        protected static GetOnePersonQueryHandler QueryOneHandler(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetOnePersonQueryHandler(repository, mapper);
        }

        protected static GetPersonPurcashFoodQueryHandler QueryPersonFoodPurcash(IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new GetPersonPurcashFoodQueryHandler(repository, mapper);
        }

        protected static CreatePersonCommandHandler CreatePersonCommandHandlerData(IContextDB _context, IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new CreatePersonCommandHandler(Substitute.For<IContextDB>(), mapper, Substitute.For<IPessoasRepository>());
        }

        protected static UpdatePersonCommandHandler UpdatePersonCommandHandlerData(IContextDB _context, IPessoasRepository repository)
        {
            var mapperConf = new MapperConfiguration(conf => conf.AddProfile<PersonProfile>());
            var mapper = new Mapper(mapperConf);
            return new UpdatePersonCommandHandler(
                Substitute.For<IContextDB>(), 
                repository ?? Substitute.For<IPessoasRepository>(), 
                mapper);
        }

        protected static DeletePersonCommandHandler DeleteHandlePersonCommand(IContextDB context, IPessoasRepository repository)
        {
            return new DeletePersonCommandHandler(
                context ?? Substitute.For<IContextDB>(), 
                repository ?? Substitute.For<IPessoasRepository>());
        }

        private static GetPersonPurcashFoodQueryHandler CreateHandlerGetPersonPurcashFoodExceptionGetOnePessoa(
            IPessoasRepository repository = default, IMapper mapper = default)
        {
            return new GetPersonPurcashFoodQueryHandler(
                repository ?? Substitute.For<IPessoasRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        private static GetOnePersonQueryHandler CreateHandlerExceptionGetOnePessoa(IPessoasRepository repository = default, IMapper mapper = default)
        {
            return new GetOnePersonQueryHandler(
                repository ?? Substitute.For<IPessoasRepository>(),
                mapper ?? Substitute.For<IMapper>());
        }

        protected static CreatePersonCommandHandler CreateHandlerSetPersonExceptionGetOnePessoa(
            IContextDB context = default, IMapper mapper = default, IPessoasRepository repository = default)
        {
            return new CreatePersonCommandHandler(
                context ?? Substitute.For<IContextDB>(), 
                mapper, 
                repository ?? Substitute.For<IPessoasRepository>());
        }
        
        protected static UpdatePersonCommandHandler CreateHandlerUpdatePersonExceptionGetOnePessoa(
            IContextDB context = default, IMapper mapper = default, IPessoasRepository repository = default)
        {
            return new UpdatePersonCommandHandler(
                context ?? Substitute.For<IContextDB>(),
                repository ?? Substitute.For<IPessoasRepository>(), 
                mapper);
        }
    }
}
