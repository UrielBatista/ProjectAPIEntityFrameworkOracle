using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pro.Search.Infraestructure.Context;
using Pro.Search.Infraestructure.Repositories.Support;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.PessoasAPI.UnitTest.Repository.FoodsRepositoryTests
{
    [TestClass]
    public sealed class FoodRepositoryTest
    {
        [TestMethod]
        public async Task FindOneAsyncFoodReturnOk()
        {
            // Arrange
            var id_food = "456852";
            using var context = GetContextData();
            var repository = CreateFoodsRepository(context);

            // Act
            var result = await repository.FindOneAsyncFood(id_food, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<Food>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindAllAsyncFoodReturnOk()
        {
            // Arrange
            using var context = GetContextData();
            var repository = CreateFoodsRepository(context);

            // Act
            var result = await repository.FindAllAsyncFood(CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<List<Food>>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindAllAsyncFoodReferenceToPersonReturnOk()
        {
            // Arrange
            var id_pessas = "0002";
            using var context = GetContextData();
            var repository = CreateFoodsRepository(context);

            // Act
            var result = await repository.FindAllAsyncFoodReferenceToPerson(id_pessas, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<List<Food>>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindOneAsyncFoodReferenceToPersonReturnOk()
        {
            // Arrange
            var id_pessas = "0002";
            using var context = GetContextData();
            var repository = CreateFoodsRepository(context);

            // Act
            var result = await repository.FindOneAsyncFoodReferenceToPerson(id_pessas, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<Food>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindListFoodReferenceToIDFoodReturnOk()
        {
            // Arrange
            var id_food = "456852";
            using var context = GetContextData();
            var repository = CreateFoodsRepository(context);

            // Act
            var result = await repository.FindListFoodReferenceToIDFood(id_food, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<List<Food>>().And.NotBeNull();
            }
        }

        private static ContextDB GetContextData()
        {
            var options = new DbContextOptionsBuilder<ContextDB>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ContextDB(options);

            var id_food = "456852";
            var id_pessoas_references = "0002";
            var foods = new Food()
            {
                Id_Food = id_food,
                Id_Pessoas_References = id_pessoas_references
            };
            _ = context.Food.Add(foods);

            _ = context.SaveChanges();

            return context;
        }

        private static FoodRepository CreateFoodsRepository(IContextDB contextDB)
            => new FoodRepository(contextDB ?? Substitute.For<IContextDB>());
    }
}
