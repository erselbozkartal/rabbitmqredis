using Service;
using System;
using System.Threading.Tasks;

namespace ConsumerService
{
    class Program
    {
        private static Consumer Consumer;

        static void Main(string[] args)
        {
            Console.Write("Parelel Consumer Sayısı : ");
            var consumerCount = long.Parse(Console.ReadLine());
            Parallel.For(0, consumerCount, i =>
            {
                Console.WriteLine($"{i} Numaralı Servis Başladı");
                Consumer = new Consumer(RabbitConfig.QueueName, false, i.ToString());
            });
            //var dursun = Console.ReadLine();
            //if (int.Parse(dursun) == 1)
            //    Consumer = new Consumer(RabbitConfig.QueueName, true);
            //else
            //    Consumer = new Consumer(RabbitConfig.QueueName, false);
        }
    }
}