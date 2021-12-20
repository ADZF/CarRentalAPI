using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalAPI.Models
{
    [Table("Tb_M_Customer")]
    public class Customer
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string  Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Rental> Rental { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
    

