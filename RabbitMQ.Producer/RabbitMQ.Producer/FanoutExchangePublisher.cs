using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    class FanoutExchangePublisher
    {
        public string Exchange;
        public IModel Channel;

        public FanoutExchangePublisher(string exchange, IModel channel)
        {
            Exchange = exchange;
            Channel = channel;
            var ttl = new Dictionary<string, object> { { "x-message-ttl", 30000 } };
            Channel.ExchangeDeclare(Exchange, ExchangeType.Fanout, arguments: ttl);
        }

        public void Publish()
        {
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                Console.WriteLine(message);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                Channel.BasicPublish(Exchange, string.Empty, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
