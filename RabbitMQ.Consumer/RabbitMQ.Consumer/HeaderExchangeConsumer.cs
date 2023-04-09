using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class HeaderExchangeConsumer
    {
        public string Queue;
        public string Exchange;
        public IModel Channel;
        public Dictionary<string, object> Header;

        public HeaderExchangeConsumer(string queue, string exchange, IModel channel, Dictionary<string, object> header)
        {
            Exchange = exchange;
            Queue = queue;
            Channel = channel;
            Header = header;
            Channel.ExchangeDeclare(Exchange, ExchangeType.Headers);
            Channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            
            Channel.QueueBind(Queue, Exchange, string.Empty, header);
            Channel.BasicQos(0, 10, false);
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            Channel.BasicConsume(Queue, true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
