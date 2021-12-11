using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PessoasAPI.Controllers.V1;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasPurcashControllerTests
{
    [TestClass]
    public class TestBase
    {
        protected static PessoasPurcashController CreateController(IMediator mediator)
        => new PessoasPurcashController(mediator ?? Substitute.For<IMediator>());
    }
}
