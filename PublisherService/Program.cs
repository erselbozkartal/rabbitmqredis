using Service;
using System;
using System.Threading.Tasks;

namespace PublisherService
{
    class Program
    {
        private static Publisher Publisher;

        static void Main(string[] args)
        {
            Console.Write("Parelel Publisher Sayısı : ");
            var publisherCount = long.Parse(Console.ReadLine());
            Parallel.For(0, publisherCount, i =>
            {
                Console.WriteLine($"{i} Numaralı Servis Başladı");
                Publisher = new Publisher(RabbitConfig.QueueName, "Hello RabbitMQ World!");
            });
            Console.ReadLine();
        }
    }
}