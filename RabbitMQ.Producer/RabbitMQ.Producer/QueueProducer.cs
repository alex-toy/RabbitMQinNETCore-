﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public class QueueProducer
    {
        private const string Queue = "demo-queue";

        public static void Publish(IModel channel)
        {
            channel.QueueDeclare(Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", Queue, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
