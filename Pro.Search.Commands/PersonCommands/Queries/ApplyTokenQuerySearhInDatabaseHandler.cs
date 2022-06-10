using BuldBlocks.Domain.Commons;
using Microsoft.IdentityModel.Tokens;
using Pro.Search.Infraestructure.Repositories.Support;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using Pro.Search.PersonDomains.PersonEngine.Entities;
using Pro.Search.PersonDomains.PersonEngine.OneOf;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Pro.Search.PersonDomains.PersonEngine.OneOf.TokenRequestResponses;

namespace Pro.Search.Commands.PersonCommands.Queries
{
    public class ApplyTokenQuerySearhInDatabaseHandler : IQueryHandler<ApplyTokenQuerySearhInDatabase, TokenRequestResponses>
    {
        private readonly IUserRepository repository;
        private static List<(string, string)> _refreshTokens = new();

        public ApplyTokenQuerySearhInDatabaseHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TokenRequestResponses> Handle(ApplyTokenQuerySearhInDatabase request, CancellationToken cancellationToken)
        {
            var user = await this.repository.FindUser(request.Username, request.Password, cancellationToken);

            if (user == null)
                return new NotFound($"Not contain user {request.Username} in Database!");

            var token = GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            SaveRefreshToken(user.Username, refreshToken);
            var result = new TokenResponseDto
            {
                Username = user.Username,
                Token = token,
            };

            return new Success(result);
        }

        public static void SaveRefreshToken(string username, string refreshToken)
        {
            _refreshTokens.Add(new(username, refreshToken));
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
