using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    static class HeaderExchangePublisher
    {
        private const string Exchange = "demo-header-exchange";

        public static void Publish(IModel channel)
        {
            Dictionary<string, object>? ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare(Exchange, ExchangeType.Headers, arguments: ttl);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "update" } };

                channel.BasicPublish(Exchange, string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
