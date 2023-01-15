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

        string queueName = "demo-queue";
        channel.QueueDeclare(queueName, durable:true, exclusive:false, autoDelete:false, arguments:null);
        var message = new { Name = "Producer", Message = "Hello!" };
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        channel.BasicPublish("", queueName, null, body);

        //FanoutExchangePublisher.Publish(channel);
    }
}
