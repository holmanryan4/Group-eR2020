using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class ActivityTrnsaction
    {
        [Key]
        public int TransationId { get; set; }
        public double Purchases { get; set; }
    }
}
