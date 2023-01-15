using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer;

static class Program
{
    static void Main()
    {
        ConnectionFactory? factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        using IConnection? connection = factory.CreateConnection();
        using IModel? channel = connection.CreateModel();

        QueueProducer.Publish(channel);

        //FanoutExchangePublisher.Publish(channel);
    }
}
