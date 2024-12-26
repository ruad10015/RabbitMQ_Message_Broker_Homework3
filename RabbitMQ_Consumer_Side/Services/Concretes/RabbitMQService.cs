using DevOps_RabbitMQ_Consumer_Side.Services.Abstracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace DevOps_RabbitMQ_Consumer_Side.Services.Concretes
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

        public void StartListening(Action<string> onMessageReceived)
        {
            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is not initialized.");
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    onMessageReceived(message);

                    Console.WriteLine("Message received: " + message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            };

            try
            {
                _channel.BasicConsume(queue: "my-queue",
                                      true,
                                      consumer: consumer);

                Console.WriteLine("Consumer started listening to 'my-queue'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while starting consumer: {ex.Message}");
            }
        }
        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
