﻿using RabbitMQ.Client;

namespace RabbitMQ.Producer;

static class Program
{
    static void Main()
    {
        IModel channel = CreateChannel("amqp://guest:guest@localhost:5672");

        string queueName = "demo-queue";
        QueueProducer producer = new QueueProducer(queueName, channel);
        producer.Publish();

        //string routingKey = "account.init";
        //string exchange = "direct_exchange";
        //var directExchangePublisher = new DirectExchangePublisher(routingKey, exchange, channel);
        //directExchangePublisher.Publish();

        //TopicExchangePublisher.Publish(channel);
        //HeaderExchangePublisher.Publish(channel);
    }

    private static IModel CreateChannel(string url)
    {
        var factory = new ConnectionFactory { Uri = new Uri(url) };
        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();
        return channel;
    }
}
