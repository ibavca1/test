using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using luxoft_T1.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace luxoft_T1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _services;
        //TODO: Посмотреть абстракции
        private readonly IFakeRepo<Customer> _cusomer;
        private readonly IFakeRepo<Contractor> _contractor;

        public HomeController(IServiceProvider services, ILogger<HomeController> logger)
        {
            _logger = logger;
            _services = services;
            if (services != null)
            {
                _logger.LogInformation("service is not null");
                _cusomer = _services.GetRequiredService<IFakeRepo<Customer>>();
                _contractor = _services.GetRequiredService<IFakeRepo<Contractor>>();
            }
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "Тестовое задание";
            return View(_cusomer.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.id = _cusomer.GetMax() + 1;
            _cusomer.Add(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _cusomer.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
