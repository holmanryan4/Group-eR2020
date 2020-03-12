using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class UserAccountGroup
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        public UserAccount UserAccount { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
