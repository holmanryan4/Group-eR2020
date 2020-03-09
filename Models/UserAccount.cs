using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        [Required]
        public int AddressID { get; set; }
        public Address address { get; set; }
        [ForeignKey("Wallet")]
        public int WalletId { get; set; }
        public Wallet wallet { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group group { get; set; }

        [ForeignKey("Event Transaction")]
        public int EventId { get; set; }
        public Activity activity { get;set; }
        


    }
}
