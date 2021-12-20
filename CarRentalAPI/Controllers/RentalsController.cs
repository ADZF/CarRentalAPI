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
    public class RentalsController : BaseController<Rental, RentalRepository, int>
    {
        private readonly RentalRepository rentalRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public RentalsController(RentalRepository rentalRepository, IConfiguration configuration, MyContext myContext) : base(rentalRepository)
        {
            this.rentalRepository = rentalRepository;
            this.myContext = myContext;
            this._configuration = configuration;

        }
        [Route("AddRental")]
        [HttpPost]
        public ActionResult AddRental(RentalVM rentalVM)
        {
            var check = rentalRepository.AddRental(rentalVM);
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
        [HttpGet("GetIdRental/{id}")]
        public ActionResult GetIdRental(int id)
        {

            var result = rentalRepository.GetIdRental(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Data dengan Email tersebut tidak ditemukan" });

        }
    }
}
