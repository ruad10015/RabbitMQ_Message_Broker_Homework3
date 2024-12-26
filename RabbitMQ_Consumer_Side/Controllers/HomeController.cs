using DevOps_RabbitMQ_Consumer_Side.Models;
using DevOps_RabbitMQ_Consumer_Side.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevOps_RabbitMQ_Consumer_Side.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRabbitMQService _rabbitMQService;
        private static readonly List<string> _messages = new List<string>();
        public HomeController(IRabbitMQService rabbitMQService)
        {

            _rabbitMQService = rabbitMQService;
            _rabbitMQService.StartListening(OnMessageReceived);
        }

        private void OnMessageReceived(string message)
        {
            _messages.Add(message);
        }

        public IActionResult Index()
        {
            return View(_messages);
        }
    }
}
