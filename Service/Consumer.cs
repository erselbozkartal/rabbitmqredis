using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RedisService;
using System;
using System.Text;

namespace Service
{
    public class Consumer
    {
        public Consumer(string queueName, bool dursun, string consumerNum)
        {
            var i = 0;
            var rabbitMqService = new RabbitMqService();
            using (var connection = rabbitMqService.GetRabbitMQConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                 {
                     var body = ea.Body;
                     var message = Encoding.UTF8.GetString(body);
                     var props = ea.BasicProperties;
                     var hata = HeadOrTail();
                     if (hata)
                     {
                         if (RedisCache.Instance.Add(props.CorrelationId, "1"))
                         {
                             Console.WriteLine($"Queue = {queueName}, Message = {message}");
                             hata = HeadOrTail();
                             if (hata)
                                 if (dursun)
                                 {
                                     if (i <= 5)
                                     {
                                         if (Repository.UpdateValue(consumerNum))
                                             channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                                         else
                                         {
                                             channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                                             RedisCache.Instance.Remove(props.CorrelationId);
                                         }
                                         i++;
                                     }
                                     else
                                         RedisCache.Instance.Remove(props.CorrelationId);
                                 }
                                 else
                                 {
                                     if (Repository.UpdateValue(consumerNum))
                                         channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                                     else
                                     {
                                         channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                                         RedisCache.Instance.Remove(props.CorrelationId);
                                     }
                                 }
                             else
                             {
                                 channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                                 RedisCache.Instance.Remove(props.CorrelationId);
                             }
                         }
                         else channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                     }
                     else channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                 };
                channel.BasicConsume(queueName, false, consumer);
                Console.ReadLine();
            }
        }

        private bool HeadOrTail()
        {
            var result = new Random().Next(0, 2);
            if (result == 0) return true;
            else
            {
                Console.WriteLine("Hata Meydana Geldi");
                return false;
            }
        }
    }
}