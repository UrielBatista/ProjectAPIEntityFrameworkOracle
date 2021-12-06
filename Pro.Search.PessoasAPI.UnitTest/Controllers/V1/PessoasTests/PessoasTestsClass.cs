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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasTests
{
    [TestClass]
    public sealed class PessoasTestsClass : TestBase
    {
        [TestMethod]
        public async Task GetPessoasShouldReturnOk()
        {
            // Prepare
            var results = new List<PessoasAllInfoDto>
            {
                new PessoasAllInfoDto
                {
                    Id_Pessoas = "151251",
                    Nome = "TesteGetMethodController1",
                    Sobrenome = "TesteGetMethodController2",
                    Pessoas_Calc_Number = 12.7M,
                    DataHora = DateTime.Now
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<GetAllPersonQuery>()).Returns(results);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.ListarPessoas().ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<PessoasAllInfoDto>>();
            }
        }

        [TestMethod]
        public async Task GetPessoasShouldReturnNotFound()
        {
            // Prepare
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<GetAllPersonQuery>()).Returns((List<PessoasAllInfoDto>)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.ListarPessoas().ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }

        [TestMethod]
        public async Task GetOnePessoasShoudReturnOk()
        {
            // Prepare
            var id_pessoa = "16";
            var result = new PersonDto
            {
                Pessoas = new PessoasInfoDto
                {
                    Id_Pessoas = "613",
                    Nome = "TesteGetMethodController3",
                    Sobrenome = "TesteGetMethodController4",
                    Pessoas_Calc_Number = 72.7M,
                    DataHora = DateTime.Now
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<GetOnePersonQuery>(x => x.Id_Pessoas == id_pessoa)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.BuscarUmaPessoa(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<PersonDto>();
            }
        }

        [TestMethod]
        public async Task GetOnePessoasShouldReturnNotFound()
        {
            // Prepare
            var id_pessoa = "923";

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<GetOnePersonQuery>(x => x.Id_Pessoas == id_pessoa)).Returns((PersonDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.BuscarUmaPessoa(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }

        [TestMethod]
        public async Task GetMediaPessoasShouldReturnOk()
        {
            // Prepare
            decimal result = 56.9M;
            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Any<GetMediaPersonQuery>()).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.CalcMedia().ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>();
            }
        }

        [TestMethod]
        public async Task CreatePessoasShouldReturnOk()
        {
            // Prepare
            var result = new PersonDto
            {
                Pessoas = new PessoasInfoDto
                {
                    Id_Pessoas = "333",
                    Nome = "TesteGetMethodController5",
                    Sobrenome = "TesteGetMethodController6",
                    Pessoas_Calc_Number = 33.3M,
                    DataHora = DateTime.Now
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<CreatePersonCommand>(x => x.PersonDto == result)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.InserirPessoas(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>();
            }
        }

        [TestMethod]
        public async Task CreatePessoasShouldReturnBadRequest()
        {
            // Prepare
            var result = new PersonDto
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<CreatePersonCommand>(x => x.PersonDto == result)).Returns((PersonDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.InserirPessoas(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<BadRequestResult>();
            }
        }

        [TestMethod]
        public async Task UpdatePessoasShouldReturnOk()
        {
            // Prepare
            var result = new PersonDto
            {
                Pessoas = new PessoasInfoDto
                {
                    Id_Pessoas = "771",
                    Nome = "TestControllerMethod7",
                    Sobrenome = "TestControllerMethod8",
                    Pessoas_Calc_Number = 11.1M,
                    DataHora = DateTime.Now
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<UpdatePersonCommand>(x => x.PersonDto == result)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.AtualizarPessoasAsync(result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>();
            }
        }

        [TestMethod]
        public async Task DeletePessoasShouldReturnOK()
        {
            // Perpare
            var id_pessoa = "601";
            var results = new List<Pessoas>
            {
                new Pessoas
                {
                    Id_Pessoas = "812",
                    Nome = "TestControllerDeletePessoas1",
                    Sobrenome = "TestControllerDeletePessoas2",
                    Pessoas_Calc_Number = 99.9M,
                    DataHora = DateTime.Now,
                    ComidaComprada = new List<Food>
                    {
                        new Food
                        {
                            Id_Food = "151",
                            Name_Food = "TestControllerDeleteFood1",
                            Locale_Purcache_Food = "TestControllerDeleteFood2",
                            Id_Pessoas_References = "9191",
                            Price_Food = 555.2M
                        },
                    },
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<DeletePersonCommand>(x => x.Id_Pessoas == id_pessoa)).Returns(results);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.DeletearPessoas(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<Pessoas>>();
            }
        }

        [TestMethod]
        public async Task DeletePessoasShouldReturnNotFound()
        {
            // Prepare
            var id_pessoa = "601";
            var results = new List<Pessoas>
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<DeletePersonCommand>(x => x.Id_Pessoas == id_pessoa)).Returns(results);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.DeletearPessoas(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
