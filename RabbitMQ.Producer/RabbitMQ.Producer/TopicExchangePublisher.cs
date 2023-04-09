using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    class TopicExchangePublisher
    {
        public string RoutingKey;
        public string Exchange;
        public IModel Channel;

        public TopicExchangePublisher(string routingKey, string exchange, IModel channel)
        {
            RoutingKey = routingKey;
            Exchange = exchange;
            Channel = channel;
            var ttl = new Dictionary<string, object> { { "x-message-ttl", 30000 } };
            Channel.ExchangeDeclare(Exchange, ExchangeType.Topic, arguments: ttl);
        }

        public void Publish()
        {
            Console.WriteLine("Producer started");
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                Console.WriteLine(message);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                Channel.BasicPublish(Exchange, RoutingKey, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
