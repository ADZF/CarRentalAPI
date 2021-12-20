using CarRentalAPI.Context;
using CarRentalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    public class CarRepository : GeneralRepository<MyContext, Car, string>
    {
        public CarRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
