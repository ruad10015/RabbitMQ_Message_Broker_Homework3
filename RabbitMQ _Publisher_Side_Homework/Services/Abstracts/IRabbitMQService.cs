using RabbitMQ.Client;
using System.Text;

namespace DevOps_RabbitMQ_Publisher_Side_Homework.Services.Abstracts
{
    public interface IRabbitMQService
    {
        void SendMessage(string message);
        void Close();
    }

}
