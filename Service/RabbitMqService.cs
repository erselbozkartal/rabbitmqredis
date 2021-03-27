using RabbitMQ.Client;

namespace Service
{
    public class RabbitMqService
    {
        private readonly string _hostName = "127.0.0.1";

        public IConnection GetRabbitMQConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };
            return connectionFactory.CreateConnection();
        }
    }
}