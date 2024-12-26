using DevOps_RabbitMQ_Publisher_Side_Homework.Services.Abstracts;
using RabbitMQ.Client;
using System.Text;

namespace DevOps_RabbitMQ_Publisher_Side_Homework.Services.Concretes
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "",
                UserName = "",
                Password = "",
                VirtualHost = "",               
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "my-queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: "my-queue",
                                  basicProperties: null,
                                  body: body);
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
