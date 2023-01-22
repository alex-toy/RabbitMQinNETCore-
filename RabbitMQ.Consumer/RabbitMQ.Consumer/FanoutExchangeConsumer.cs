using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        private const string Exchange = "demo-fanout-exchange";
        private const string Queue = "demo-fanout-queue";

        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(Exchange, ExchangeType.Fanout);
            channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(Queue, Exchange, string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
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
