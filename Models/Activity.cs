using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId {get;set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public double Purchases { get; set; }
    }
}
