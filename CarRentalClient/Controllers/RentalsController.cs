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
    public class RentalsController : BaseController<Rental, RentalRepository, int>
    {
        private readonly RentalRepository repository;
        public RentalsController(RentalRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public JsonResult AddRental(RentalVM entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }
        public async Task<JsonResult> GetidRental(string id)
        {
            var result = await repository.GetRental(id);
            return Json(result);
        }
    }
}
