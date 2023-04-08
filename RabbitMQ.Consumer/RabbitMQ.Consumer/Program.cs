using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            IModel channel = CreateChannel("amqp://guest:guest@localhost:5672");

            QueueConsumer queueConsumer = new QueueConsumer("demo-queue", ProcessEvent);
            queueConsumer.Consume(channel);

            //string queue = "direct_exchange_queue";
            //string exchange = "direct_exchange";
            //string routingKey = "account.init";
            //var directExchangeConsumer = new DirectExchangeConsumer(queue, exchange, routingKey, channel);
            //directExchangeConsumer.Consume();

            //TopicExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);

            Console.ReadLine();
        }

        private static IModel CreateChannel(string url)
        {
            var factory = new ConnectionFactory { Uri = new Uri(url) };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            return channel;
        }

        private static void ProcessEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}   