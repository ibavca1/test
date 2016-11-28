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

        private readonly IFakeRepo<Customer> _service;

        private readonly MyModel model;

        public HomeController(IServiceProvider services, ILogger<HomeController> logger)
        {
            _logger = logger;
            _services = services;
            model = new MyModel();
            if (services != null)
            {
                _logger.LogInformation("service is not null");
                model.listCustomer = _services.GetRequiredService<IFakeRepo<Customer>>();
                model.listContractor = _services.GetRequiredService<IFakeRepo<Contractor>>();
            }
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "Тестовое задание";
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            switch (Request.Query.Keys.FirstOrDefault())
            {
                case "listCustomer":
                    var customer = new Customer();
                    return View("createCustomer",customer);
                case "listContractor":
                    var contractor = new Contractor();
                    return View("createContractor", contractor);
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateContractor(Contractor contractor)
        {
            contractor.id = model.listContractor.GetMax() + 1;
            model.listContractor.Add(contractor);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            customer.id = model.listCustomer.GetMax() + 1;
            model.listCustomer.Add(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            model.listCustomer.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteContractor(int id)
        {
            model.listContractor.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditContractor(int id)
        {
            var contractor = model.listContractor.GetById(id);
            return View("editContractor",contractor);
        }

        [HttpPost]
        public IActionResult EditContractor(Contractor contractor)
        {
            model.listContractor.Edit(contractor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = model.listCustomer.GetById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            model.listCustomer.Edit(customer);
            return RedirectToAction("Index");
        }
    }
}
