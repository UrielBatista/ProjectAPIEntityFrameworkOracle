using System.Threading;
using System.Threading.Tasks;
using Pro.Search.PersonDomains.PersonEngine.Dtos;
using HotChocolate.Subscriptions;
using AutoMapper;

namespace Pro.Search.Infraestructure.Services
{
    public class TopicCreatePersonSubscriptionService : ITopicCreatePersonSubscriptionService
    {
        private readonly ITopicEventSender topicEventSender;
        private readonly IMapper mapper;

        public TopicCreatePersonSubscriptionService(ITopicEventSender topicEventSender, IMapper mapper)
        {
            this.topicEventSender = topicEventSender;
            this.mapper = mapper;
        }

        async Task ITopicCreatePersonSubscriptionService.PublishTopicCreatePerson(
            PersonsInfoDto personDto, 
            CancellationToken cancellationToken)
        {
            var personSubscription = this.mapper.Map<PersonsInfoDto, GraphQLPersonsInfoDto>(personDto);
            await this.topicEventSender.SendAsync("CreatedGraphQLPerson", personSubscription).ConfigureAwait(false);
        }
    }
}
