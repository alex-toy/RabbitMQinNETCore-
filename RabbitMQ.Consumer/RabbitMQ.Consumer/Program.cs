using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            IModel channel = CreateChannel();

            //QueueConsumer.Consume(channel);
            //DirectExchangeConsumer.Consume(channel);
            TopicExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);

            Console.ReadLine();
        }

        private static IModel CreateChannel()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            return channel;
        }
    }
}   