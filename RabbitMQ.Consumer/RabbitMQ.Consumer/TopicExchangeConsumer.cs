using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class TopicExchangeConsumer
    {
        public string Queue;
        public string RoutingKey;
        public string Exchange;
        public IModel Channel;

        public TopicExchangeConsumer(string queue, string routingKey, string exchange, IModel channel)
        {
            Queue = queue;
            RoutingKey = routingKey;
            Exchange = exchange;
            Channel = channel;
            Channel.ExchangeDeclare(Exchange, ExchangeType.Topic);
            Channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            Channel.QueueBind(Queue, Exchange, RoutingKey);
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
