using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class FanoutExchangeConsumer
    {
        public string Exchange;
        public string Queue;
        public IModel Channel;

        public FanoutExchangeConsumer(string queue, string exchange, IModel channel)
        {
            Exchange = exchange;
            Queue = queue;
            Channel = channel;
            Channel.ExchangeDeclare(Exchange, ExchangeType.Fanout);
            Channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            Channel.QueueBind(Queue, Exchange, string.Empty);
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
