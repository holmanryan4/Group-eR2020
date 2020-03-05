using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper_Capstone.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Address")]
        [Required]
        public int AddressId { get; set; }
        [ForeignKey("PaymentId")]
        public int Payment { get; set; }
        [ForeignKey("MemoryId")]
        [Required]
        public int Zipcode { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public float Wallet { get; set; }
        public float MemoryFund { get; set; }

    }
}
