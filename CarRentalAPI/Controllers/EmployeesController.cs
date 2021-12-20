using CarRentalAPI.Context;
using CarRentalAPI.Controller.Base;
using CarRentalAPI.Models;
using CarRentalAPI.Repository.Data;
using CarRentalAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
            this._configuration = configuration;
        }
        [Route("RegisterEmployee")]
        [HttpPost]
        public ActionResult RegisterEmployee(AddEmployeeVM registerUserVM)
        {
            var check = employeeRepository.RegisterEmployee(registerUserVM);
            if (check == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data berhasil ditambahkan" });
            }
            if (check == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal ditambahkan. Email sudah terdaftar" });
            }
            else
               return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal ditambahkan." });
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult LoginEmp(LoginVM loginEmpVM)
        {
            var result = employeeRepository.Login(loginEmpVM);
            if (result == 2)
            {
                return NotFound(new JWTokenVM { Token = "", Messages = "0" });
            }
            else if (result == 3)
            {
                var getDataUser = (from e in myContext.Employees
                                   join r in myContext.Roles on e.RoleId equals r.RoleId
                                   where e.Email == loginEmpVM.Email
                                   select new
                                   {
                                       NIK = e.NIK,
                                       Email = e.Email,
                                       Role = r.RoleName
                                   });
                var role = (from e in myContext.Employees
                            join r in myContext.Roles on e.RoleId equals r.RoleId
                            where e.Email == loginEmpVM.Email
                            select new
                            {
                                Role = r.RoleName
                            }).Single().ToString();

                var data = new LoginVM()
                {
                    Email = loginEmpVM.Email,
                    Role = role
                };
                var claims = new List<Claim>
                 {
                new Claim("email", data.Email),
                };
                    claims.Add(new Claim("Roles", data.Role));
                    claims.Add(new Claim(ClaimTypes.Name, employeeRepository.GetName(loginEmpVM.Email)));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, employeeRepository.GetId(loginEmpVM.Email)));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );
                var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                return Ok(new JWTokenVM
                {
                    Token = idToken,
                    Messages = "Login Berhasil!!",
                    NIK = employeeRepository.GetEmpId(loginEmpVM)
                });
            }
            return NotFound(new JWTokenVM { Token = "", Messages = "1" });
        }
    }
}
