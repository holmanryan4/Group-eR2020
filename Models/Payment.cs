using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int CCNumber { get; set; }
        public int ExpDate { get; set; }
        public int CvcNumber { get; set; }
    }
}
