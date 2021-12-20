using CarRentalAPI.Context;
using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    public class CustomerRepository : GeneralRepository<MyContext, Customer, string>
    {
        private readonly MyContext myContext;
        public CustomerRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int AddCustomer(AddCustomerVM addCustomerVM)
        {
            Customer customer = new Customer();
            var checkEmail = myContext.Customers.Where(x => x.Email == addCustomerVM.Email).FirstOrDefault();
            var checkPhone = myContext.Customers.Where(x => x.Phone == addCustomerVM.Phone).FirstOrDefault();
            customer.Email = addCustomerVM.Email;

            if (checkEmail != null)
            {
                return 2;
            }
            if (checkPhone != null)
            {
                return 3;
            }

            customer.NIK = addCustomerVM.NIK;
            customer.FirstName = addCustomerVM.FirstName;
            customer.LastName = addCustomerVM.LastName;
            customer.BirthDate = addCustomerVM.BirthDate;
            customer.Gender = (Models.Gender)addCustomerVM.Gender;
            customer.Phone = addCustomerVM.Phone;
            customer.Email = addCustomerVM.Email;
            customer.Address = addCustomerVM.Address;

            myContext.Customers.Add(customer);
            var result = myContext.SaveChanges();
            return result;
        }
        public IEnumerable<RentalVM> GetRentStatus()
        {
            var payStatus = (from c in myContext.Customers
                              join r in myContext.Rental on
                              c.NIK equals r.CustomerId
                             
                              select new RentalVM
                              {
                                  OrderId = r.OrderId,
                                  CustomerId = c.NIK,
                                  FirstName = c.FirstName,
                                  LastName = c.LastName,
                                  Phone = c.Phone,
                                  Email = c.Email,
                                  RentDate = r.RentDate,
                                  ReturnDate = r.ReturnDate,
                                  Status = r.Status,
                              }).Where(r => r.Status == 0).ToList();
            return payStatus;
        }

        public IEnumerable<AddCustomerVM> GetIdProfile(string NIK)
        {
            var getProfile = (from c in myContext.Customers
                              select new AddCustomerVM
                              {
                                  NIK = c.NIK,
                                  FirstName = c.FirstName,
                                  LastName = c.LastName,
                                  Email = c.Email,
                                  Phone = c.Phone,
                                  Gender = (ViewModel.Gender)c.Gender,
                                  BirthDate = c.BirthDate,
                                  Address = c.Address,
                              }).Where(c => c.NIK == NIK).ToList();

            return getProfile;
        }
    }
}
