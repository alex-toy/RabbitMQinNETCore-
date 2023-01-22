using RabbitMQ.Client;

namespace RabbitMQ.Producer;

static class Program
{
    static void Main()
    {
        IModel channel = CreateChannel();

        //QueueProducer.Publish(channel);
        DirectExchangePublisher.Publish(channel);
    }

    private static IModel CreateChannel()
    {
        ConnectionFactory? factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();
        return channel;
    }
}
