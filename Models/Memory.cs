using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Memory
    {
        [Key]
        public int MemoryId { get; set; }
        public double Balance { get; set; }
    }
}
