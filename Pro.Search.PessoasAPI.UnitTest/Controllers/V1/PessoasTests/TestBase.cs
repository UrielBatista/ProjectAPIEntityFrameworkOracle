using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PessoasAPI.Controllers.V1;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.PessoasTests
{
    [TestClass]
    public class TestBase
    {
        protected static PessoasController CreateController(IMediator mediator)
        => new PessoasController(mediator ?? Substitute.For<IMediator>());
    }
}
