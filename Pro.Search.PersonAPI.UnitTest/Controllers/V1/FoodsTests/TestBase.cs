using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PessoasAPI.Controllers.V1;

namespace Pro.Search.PessoasAPI.UnitTest.Controllers.V1.FoodsTests
{
    [TestClass]
    public class TestBase
    {
        protected static FoodController CreateController(IMediator mediator)
            => new FoodController(mediator ?? Substitute.For<IMediator>());
    }
}
