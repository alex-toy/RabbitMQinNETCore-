using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    class HeaderExchangePublisher
    {
        public string Exchange;
        public IModel Channel;
        public Dictionary<string, object> Header;

        public HeaderExchangePublisher(string exchange, IModel channel, Dictionary<string, object> header)
        {
            Exchange = exchange;
            Channel = channel;
            Header = header;
            var ttl = new Dictionary<string, object> { { "x-message-ttl", 30000 } };
            Channel.ExchangeDeclare(Exchange, ExchangeType.Headers, arguments: ttl);
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

                var properties = Channel.CreateBasicProperties();
                properties.Headers = Header;

                Channel.BasicPublish(Exchange, string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
