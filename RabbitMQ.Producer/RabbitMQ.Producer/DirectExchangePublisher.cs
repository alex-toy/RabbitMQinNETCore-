using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public class DirectExchangePublisher
    {
        private string RoutingKey;
        private string Exchange;
        public IModel Channel;

        public DirectExchangePublisher(string routingKey, string exchange, IModel channel)
        {
            RoutingKey = routingKey;
            Exchange = exchange;
            Channel = channel;
            Dictionary<string, object>? ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            Channel.ExchangeDeclare(Exchange, ExchangeType.Direct, arguments: ttl);
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
