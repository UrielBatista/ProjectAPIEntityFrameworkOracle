using Pro.Search.PersonDomains.PersonEngine.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Search.Infraestructure.Services
{
    public interface ITopicCreatePersonSubscriptionService
    {
        Task PublishTopicCreatePerson(
            PersonsInfoDto personDto,
            CancellationToken cancellationToken);
    }
}
