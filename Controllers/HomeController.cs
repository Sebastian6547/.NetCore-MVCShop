using DutchTreat.Data;
using DutchTreat.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IDutchRepository _repository;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IDutchRepository repository)
        {
            _logger = logger;
            _emailSender = emailSender;
            _repository = repository;
        }

        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();

            return View(results);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public async Task<IActionResult> ContactAsync(ContactModel contact)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(contact.Email, contact.Topic, contact.Message);
                return View("Success", contact);

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
