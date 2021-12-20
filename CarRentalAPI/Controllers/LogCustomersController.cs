using CarRentalAPI.Controller.Base;
using CarRentalAPI.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogCustomersController : BaseController<LogCustomer, LogCustomerRepository, int>
    {
        public LogCustomersController(LogCustomerRepository logCustomerRepository) : base(logCustomerRepository)
        {

        }
    }
}
