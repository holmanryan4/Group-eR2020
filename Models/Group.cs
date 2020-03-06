using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Event Transaction")]
        public int EventId { get; set; }
        public Activity activity { get; set; }

    }
}
