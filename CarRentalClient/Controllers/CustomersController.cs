using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using CarRentalClient.Base.Controllers;
using CarRentalClient.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalClient.Controllers
{
    public class CustomersController : BaseController<Customer, CustomerRepository, string>
    {
        private readonly CustomerRepository repository;
        public CustomersController(CustomerRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public JsonResult AddCustomer(AddCustomerVM entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }
        public async Task<JsonResult> GetUser(string id)
        {
            var result = await repository.GetUser(id);
            return Json(result);
        }
    }
}
