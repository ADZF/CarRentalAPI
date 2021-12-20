using CarRentalAPI.Context;
using CarRentalAPI.Models;
using CarRentalAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int RegisterEmployee(AddEmployeeVM addEmployeeVM)
        {
            Employee employee = new Employee();
            var checkEmail = myContext.Employees.Where(x => x.Email == addEmployeeVM.Email).FirstOrDefault();
            employee.Email = addEmployeeVM.Email;

            if (checkEmail != null)
            {
                return 2;
            }

            employee.NIK = addEmployeeVM.NIK;
            employee.FirstName = addEmployeeVM.FirstName;
            employee.LastName = addEmployeeVM.LastName;
            employee.Email = addEmployeeVM.Email;
            employee.Password = BCrypt.Net.BCrypt.HashPassword(addEmployeeVM.Password, GetRandomSalt());
            employee.RoleId = 1;

            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Login(LoginVM loginVM)
        {
            Employee employee = new Employee();
            var checkEmail = myContext.Employees.Where(x => x.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 2;
            }
            var checkNik = checkEmail.NIK;
            var checkPass = myContext.Employees.Find(checkEmail.NIK);
            bool validPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, checkPass.Password);
            if (validPass)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        public string GetName(string email)
        {
            var checkName = myContext.Employees.Where(e => e.Email == email).FirstOrDefault();
            return checkName.FirstName + " " + checkName.LastName;
        }
        public string GetId(string email)
        {
            var checkEmail = myContext.Employees.Where(e => e.Email == email).FirstOrDefault();
            return checkEmail.NIK.ToString();
        }
        public string GetEmpId(LoginVM loginEmpVM)
        {
            var id = (from u in myContext.Employees where u.Email == loginEmpVM.Email select u.NIK).FirstOrDefault();
            return id;
        }
    }
}
