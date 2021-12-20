using CarRentalAPI.Context;
using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    public class RentalRepository : GeneralRepository<MyContext, Rental, int>
    {
        private readonly MyContext myContext;
        public RentalRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int AddRental(RentalVM rentalVM)
        {
            Rental rental = new Rental();
            rental.CustomerId = rentalVM.CustomerId;
            rental.CarId = rentalVM.CarId;
            rental.RentDate = rentalVM.RentDate;
            rental.ReturnDate = rentalVM.ReturnDate;
            rental.Status = 0;

            LogCustomer logCustomer = new LogCustomer();
            logCustomer.RentDate = rentalVM.RentDate;
            logCustomer.ReturnDate = rentalVM.RentDate;
            logCustomer.OrderId = rental.OrderId;

            myContext.Rental.Add(rental);
            var result = myContext.SaveChanges();
            return result;
        }
        public IEnumerable<RentalVM> GetIdRental(int id)
        {
            var getRental = (from r in myContext.Rental
                             join c in myContext.Customers on r.CustomerId equals c.NIK
                             join cr in myContext.Cars on r.CarId equals cr.CarId
                              select new RentalVM
                              {
                                  OrderId = r.OrderId,
                                  NIK = c.NIK,
                                  FirstName = c.FirstName,
                                  LastName = c.LastName,
                                  Phone = c.Phone,
                                  Email = c.Email,
                                  RentDate = r.RentDate,
                                  ReturnDate = r.ReturnDate,
                                  Status = r.Status,
                                  CustomerId = c.NIK,
                                  CarId = cr.CarId,
                              }).Where(r => r.OrderId == id).ToList();

            return getRental;
        }
    }
}
