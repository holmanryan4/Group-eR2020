using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class Wallet
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int WalletId { get; set; }
        public double Balance { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        

        [ForeignKey("Transactions")]
        public int TransactionsId { get; set; }
        public Transactions Transactions { get; set; }
    }
}

