using CarRentalAPI.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarRentalAPI.ViewModel
{
    public class RentalVM
    {
        public int OrderId { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string CarId { get; set; }

    }
}
