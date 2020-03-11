using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class UserGroup
    {
        
       [ForeignKey("UserAccount")]
        public int UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; } 
        public virtual Group Group { get; set; }
    }
}
