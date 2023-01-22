using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        private const string RoutingKey = "account.init";
        private const string Exchange = "direct_exchange";

        public static void Publish(IModel channel)
        {
            Dictionary<string, object>? ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(Exchange, ExchangeType.Direct, arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(Exchange, RoutingKey, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
