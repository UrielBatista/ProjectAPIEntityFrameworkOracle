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

namespace Pro.Search.PessoasAPI.UnitTest.Repository.PessoasRepositoryTests
{
    [TestClass]
    public sealed class PessoasRepositoryTest
    {
        [TestMethod]
        public async Task FindPersonPurcashFoodReturnOk()
        {
            // Arrange
            var id_pessoa = "0001";
            using var context = GetContextData();
            var repository = CreatePessoasRepository(context);

            // Act
            var result = await repository.FindPersonPurcashFood(id_pessoa, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<Pessoas>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindAllAsyncPersonReturnOk()
        {
            // Arrange
            using var context = GetContextData();
            var repository = CreatePessoasRepository(context);

            // Act
            var result = await repository.FindAllAsyncPerson(CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<List<Pessoas>>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task FindOneAsyncPersonReturnOk()
        {
            // Arrange
            var id_pessoas = "0001";
            using var context = GetContextData();
            var repository = CreatePessoasRepository(context);

            // Act
            var result = await repository.FindOneAsyncPerson(id_pessoas, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<Pessoas>().And.NotBeNull();
            }
        }

        [TestMethod]
        public async Task SearchAllPersonToIdPersonReturnOk()
        {
            // Arrange
            var id_pessoas = "0001";
            using var context = GetContextData();
            var repository = CreatePessoasRepository(context);

            // Act
            var result = await repository.SearchAllPersonToIdPerson(id_pessoas, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using (new AssertionScope())
            {
                _ = result.Should().BeOfType<List<Pessoas>>().And.NotBeNull();
            }
        }

        private static ContextDB GetContextData()
        {
            var options = new DbContextOptionsBuilder<ContextDB>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ContextDB(options);

            var id_pessoa = "0001";

            var pessoas = new Pessoas()
            {
                Id_Pessoas = id_pessoa
            };
            _ = context.Pessoas.Add(pessoas);

            _ = context.SaveChanges();

            return context;
        }

        private static PessoasRepository CreatePessoasRepository(IContextDB contextDB)
            => new PessoasRepository(contextDB ?? Substitute.For<IContextDB>());
    }
}
