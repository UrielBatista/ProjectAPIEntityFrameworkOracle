using MediatR;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Collections.Generic;

namespace Pro.Search.PersonCommands.Queries
{
    public class GetAllPersonWithFoodQuery : IRequest<List<PersonsAllInfoDto>>
    {
    }
}
