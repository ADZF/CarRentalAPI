using CarRentalAPI.Context;
using CarRentalAPI.Controller.Base;
using CarRentalAPI.Models;
using CarRentalAPI.Repository.Data;
using CarRentalAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarRentalAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController<Customer, CustomerRepository, string>
    {
        private readonly CustomerRepository customerRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public CustomersController(CustomerRepository customerRepository, IConfiguration configuration, MyContext myContext) : base(customerRepository)
        {
            this.customerRepository = customerRepository;
            this.myContext = myContext;
            this._configuration = configuration;
        }
        [Route("AddCustomer")]
        [HttpPost]
        public ActionResult AddCustomer(AddCustomerVM registerUserVM)
        {
            var check = customerRepository.AddCustomer(registerUserVM);
            if (check == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditambahkan" });
            }
            if (check == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal ditambahkan. Email sudah terdaftar" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal ditambahkan. Phone sudah terdaftar" });

            }
        }
        [HttpGet("GetIdProfile/{NIK}")]
        public ActionResult GetIdProfile(string NIK)
        {

            var result = customerRepository.GetIdProfile(NIK);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Data dengan Email tersebut tidak ditemukan" });

        }

        [Route("GetCustomers")]
        [HttpGet]
        public ActionResult<RentalVM> GetRentStatus()
        {
            var result = customerRepository.GetRentStatus();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = " Data tidak ada data di tabel" });

        }

    }
}
