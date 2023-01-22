using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        private const string Queue = "direct_exchange_queue";
        private const string Exchange = "direct_exchange";
        private const string RoutingKey = "account.init";

        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(Exchange, ExchangeType.Direct);
            channel.QueueDeclare(Queue,  durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(Queue, Exchange, RoutingKey);
            channel.BasicQos(0, 10, false);

            EventingBasicConsumer? consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(Queue, true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
