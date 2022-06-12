using MassTransit;

namespace Pro.Search.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("receiver-new-person", e =>
                {
                    e.Consumer<PersonCreatedConsumer>();
                    e.PrefetchCount = 10;
                });

            });
            busControl.Start();

            Console.WriteLine("Waiting for messages...");

            while (true) ;
        }
    }
}
