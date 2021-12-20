using CarRentalAPI.Context;
using CarRentalAPI.Controller.Base;
using CarRentalAPI.Models;
using CarRentalAPI.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController<Car, CarRepository, string>
    {

        public CarsController(CarRepository carRepository) : base(carRepository)
        {

        }
    }
}
