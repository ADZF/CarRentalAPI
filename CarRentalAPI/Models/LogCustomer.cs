using CarRentalAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Repository.Data
{
    [Table("Tb_M_LogCustomer")]
    public class LogCustomer
    {
        [Key]
        public int LogCustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int OrderId { get; set; }
        public virtual Rental Rental { get; set; }
    }
}
