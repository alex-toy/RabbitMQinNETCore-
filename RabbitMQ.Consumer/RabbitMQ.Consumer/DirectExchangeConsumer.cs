using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class DirectExchangeConsumer
    {
        private string Queue;
        private string Exchange;
        private string RoutingKey;
        private IModel Channel;

        public DirectExchangeConsumer(string queue, string exchange, string routingKey, IModel channel)
        {
            Queue = queue;
            Exchange = exchange;
            RoutingKey = routingKey;
            Channel = channel;
            Channel.ExchangeDeclare(Exchange, ExchangeType.Direct);
            Channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            Channel.QueueBind(Queue, Exchange, RoutingKey);
            Channel.BasicQos(0, 10, false);
        }

        public void Consume()
        {
            EventingBasicConsumer? consumer = new EventingBasicConsumer(Channel);
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
