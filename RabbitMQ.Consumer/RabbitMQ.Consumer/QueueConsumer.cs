﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        private const string Queue = "demo-queue";

        public static void Consume(IModel channel)
        {
            channel.QueueDeclare(Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

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
