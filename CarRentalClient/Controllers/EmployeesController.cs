using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using CarRentalClient.Base.Controllers;
using CarRentalClient.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalClient.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost/*("Auth/")*/]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;
            var pesan = jwtToken.Messages;
            var empId = jwtToken.NIK;

             if (token == "" || token == null)
            {
                if (pesan == "0")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            HttpContext.Session.SetString("JWToken", token);


            return RedirectToAction("Index", "Auth");
        }

    }
}
