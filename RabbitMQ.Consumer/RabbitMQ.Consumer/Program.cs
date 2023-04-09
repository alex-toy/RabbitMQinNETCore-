using RabbitMQ.Client;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            IModel channel = CreateChannel("amqp://guest:guest@localhost:5672");

            //QueueConsumer queueConsumer = new QueueConsumer("demo-queue", ProcessEvent);
            //queueConsumer.Consume(channel);

            //string queue = "direct_exchange_queue";
            //string exchange = "direct_exchange";
            //string routingKey = "account.init";
            //var directExchangeConsumer = new DirectExchangeConsumer(queue, exchange, routingKey, channel);
            //directExchangeConsumer.Consume();

            //string Queue = "demo-topic-queue";
            //string RoutingKey = "account.*";
            //string Exchange = "demo-topic-exchange";
            //var topicExchangeConsumer = new TopicExchangeConsumer(Queue, RoutingKey, Exchange, channel);
            //topicExchangeConsumer.Consume();

            string queue = "demo-header-queue";
            string exchange = "demo-header-exchange";
            var header = new Dictionary<string, object> { { "account", "new" } };
            var headerExchangeConsumer = new HeaderExchangeConsumer(queue, exchange, channel, header);
            headerExchangeConsumer.Consume();

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