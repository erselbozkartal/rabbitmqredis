using RabbitMQ.Client;
using System;
using System.Text;

namespace Service
{
    public class Publisher
    {
        private readonly RabbitMqService RabbitMqService;

        public Publisher(string queueName, string message)
        {
            RabbitMqService = new RabbitMqService();
            using (var connection = RabbitMqService.GetRabbitMQConnection())
            using (var channel = connection.CreateModel())
            {
                var res = channel.QueueDeclare(queueName, true, false, false, null);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                for (var i = 1; i < 101; i++)
                {
                    properties.CorrelationId = Guid.NewGuid().ToString();
                    var count = new Random().Next(1, 10);
                    for (var z = 0; z < count; z++)
                    {
                        channel.BasicPublish(string.Empty, queueName, basicProperties: properties, body: Encoding.UTF8.GetBytes(i.ToString() + message));
                        Console.WriteLine($"{z} Mesaj Tekrar Edildi");
                    }
                    Console.WriteLine($"Queue = {queueName}, Message = {i} - {message}");
                }
            }
        }
    }
}