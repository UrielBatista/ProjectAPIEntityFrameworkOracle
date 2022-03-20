using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Commands.PersonCommands;
using Pro.Search.Commands.PersonCommands.Queries;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasPurcashControllerTests
{
    [TestClass]
    public sealed class PessoasPurcashContollerTest : TestBase
    {
        [TestMethod]
        public async Task PessoasPurcashedFoodReturnOk()
        {
            // Prepare
            var id_pessoa = "2242";

            var result = new PersonPurcashDto
            {
                Pessoas = new PersonsAllInfoDto
                {
                    Id_Pessoas = "412",
                    Nome = "TestingfMethodControllerPersonPurcash1",
                    Sobrenome = "TestingfMethodControllerPersonPurcash2",
                    Pessoas_Calc_Number = 12.7M,
                    DataHora = DateTime.Now
                },

                Food = new List<FoodAllInfoDto>
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "5125",
                        Nome = "TestingfMethodControllerPersonPurcash3",
                        LocalDeVenda = "TestingfMethodControllerPersonPurcash4",
                        ReferenciaIdPessoa = "TestingfMethodControllerPersonPurcash5",
                        PrecoComida = 22.78M
                    },
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<GetPersonPurcashFoodQuery>(x => x.Id_Pessoas == id_pessoa)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.PersonsPurcashedFood(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<PersonPurcashDto>();
            }
        }

        [TestMethod]
        public async Task PessoasPurcashedFoodReturnNotFound()
        {
            // Prepare
            var id_pessoa = "2242";

            var result = new PersonPurcashDto
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<GetPersonPurcashFoodQuery>(x => x.Id_Pessoas == id_pessoa)).Returns((PersonPurcashDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.PersonsPurcashedFood(id_pessoa).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }

        [TestMethod]
        public async Task PatchPessoasPurcashedFoodReturnOk()
        {
            // Prepare
            var id_pessoa = "8819";

            var result = new PersonPurcashDto
            {
                Pessoas = new PersonsAllInfoDto
                {
                    Id_Pessoas = "812",
                    Nome = "TestingfMethodControllerPersonPurcash6",
                    Sobrenome = "TestingfMethodControllerPersonPurcash7",
                    Pessoas_Calc_Number = 41.65M,
                    DataHora = DateTime.Now
                },

                Food = new List<FoodAllInfoDto>
                {
                    new FoodAllInfoDto
                    {
                        Id_Food = "9823",
                        Nome = "TestingfMethodControllerPersonPurcash8",
                        LocalDeVenda = "TestingfMethodControllerPersonPurcash9",
                        ReferenciaIdPessoa = "TestingfMethodControllerPersonPurcash10",
                        PrecoComida = 99.15M
                    },
                },
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<SetPessoasPurcashCommand>(x => x.Id_Pessoa == id_pessoa)).Returns(result);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.PersonsPurcashedFood(id_pessoa, result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<PersonPurcashDto>();
            }
        }

        [TestMethod]
        public async Task PatchPessoasPurcashedFoodReturnNotFound()
        {
            // Prepare
            var id_pessoa = "8819";

            var result = new PersonPurcashDto
            {
            };

            var mediator = Substitute.For<IMediator>();
            _ = mediator.Send(Arg.Is<SetPessoasPurcashCommand>(x => x.Id_Pessoa == id_pessoa)).Returns((PersonPurcashDto)default);

            // Assert
            var controller = CreateController(mediator);
            var response = await controller.PersonsPurcashedFood(id_pessoa, result).ConfigureAwait(false);
            using (new AssertionScope())
            {
                _ = response.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
