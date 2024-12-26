using DevOps_RabbitMQ_Publisher_Side_Homework.Models;
using DevOps_RabbitMQ_Publisher_Side_Homework.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace DevOps_RabbitMQ_Publisher_Side_Homework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRabbitMQService _rabbitMQService;
        public HomeController(ILogger<HomeController> logger, IRabbitMQService rabbitMQService)
        {
            _logger = logger;
            _rabbitMQService = rabbitMQService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Model = new SendMessageViewModel
            {
                Message = ""
            };

            return View(Model);
        }

        [HttpPost]
        public IActionResult Index(SendMessageViewModel model)
        {
            _rabbitMQService.SendMessage(model.Message);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
