using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionId { get; set; }
        public bool SentToMemory { get; set; }
        public bool SentToWallet { get; set; }
        public double TransAmount { get; set; }
    }
}
