using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace DevOps_RabbitMQ_Consumer_Side.Services.Abstracts
{
    public interface IRabbitMQService
    {
        void StartListening(Action<string> onMessageReceived);
        void Close();
    }
}
