using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class QueueConsumer
    {
        public string Queue;
        public delegate void EventProcessor(string message);
        public EventProcessor? eventProcessor;

        public QueueConsumer(string queue, EventProcessor ep)
        {
            Queue = queue;
            eventProcessor = ep;
        }

        public void Consume(IModel channel)
        {
            channel.QueueDeclare(Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            EventingBasicConsumer? consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                eventProcessor(message);
            };

            channel.BasicConsume(Queue, true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
