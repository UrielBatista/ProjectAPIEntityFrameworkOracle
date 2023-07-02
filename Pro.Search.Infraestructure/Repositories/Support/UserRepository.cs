using Microsoft.EntityFrameworkCore;
using Pro.Search.Infraestructure.Context;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Repositories.Support
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<UserEntity> users;

        public UserRepository(ISystemReadDBContext _context)
        {
            _ = _context ?? throw new ArgumentNullException(nameof(_context));
            this.users = _context.Users;
        }

        public async Task<UserEntity> FindUser(string username, string password, CancellationToken cancellationToken)
        {
            return await CreateTask(username, password, cancellationToken).ConfigureAwait(false);

            async Task<UserEntity> CreateTask(string Username, string Password, CancellationToken cancellationToken)
            {
                var userData = await this.users
                    .FirstOrDefaultAsync(p => p.Username == username && p.Password == password, cancellationToken)
                    .ConfigureAwait(false);

                return userData;
            }
        }
    }
}
