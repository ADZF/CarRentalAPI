using CarRentalAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    public class LogCustomerRepository : GeneralRepository<MyContext, LogCustomer, int>
    {
        private readonly MyContext myContext;
        public LogCustomerRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
