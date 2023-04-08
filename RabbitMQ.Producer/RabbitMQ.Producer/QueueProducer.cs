using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public class QueueProducer
    {
        public string Queue;
        public IModel Channel;

        public QueueProducer(string queue, IModel channel)
        {
            Queue = queue;
            Channel = channel;
            Channel.QueueDeclare(Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish()
        {
            Console.WriteLine("Producer started");
            var count = 0;
            while (true)
            {
                var message = new Message("Producer", $"Hello! Count: {count}");
                Console.WriteLine(message);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                Channel.BasicPublish("", Queue, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
