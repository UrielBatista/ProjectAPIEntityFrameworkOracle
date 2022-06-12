using MassTransit;
using Pro.Search.PersonDomains.PersonEngine.Events;

namespace Pro.Search.Consumer
{
    public class PersonCreatedConsumer : IConsumer<PersonCreatedEvent>
    {
        public Task Consume(ConsumeContext<PersonCreatedEvent> context)
        {
            var id = context.Message.Id_Pessoas;
            var name = context.Message.Nome;

            Console.WriteLine($"Novo usuario cadastrado: [{id}] - {name}");

            return Task.CompletedTask;
        }
    }
}
