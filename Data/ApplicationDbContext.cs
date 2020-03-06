using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Authentication.Models;

namespace Authentication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Authentication.Models.Address> Address { get; set; }
        public DbSet<Authentication.Models.Memory> Memory { get; set; }
        public DbSet<Authentication.Models.Payment> Payment { get; set; }
        public DbSet<Authentication.Models.Transactions> Transactions { get; set; }
        public DbSet<Authentication.Models.UserAccount> UserAccount { get; set; }
        public DbSet<Authentication.Models.Wallet> Wallet { get; set; }
        public DbSet<Authentication.Models.Activity> Activity { get; set; }
    }
}
