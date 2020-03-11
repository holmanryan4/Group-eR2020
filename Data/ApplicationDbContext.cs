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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<UserGroup>()
                .HasKey(bc => new { bc.UserAccountId, bc.GroupId });
            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.UserAccount)
                .WithMany(b => b.UserGroups)
                .HasForeignKey(bc => bc.UserAccountId);
            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.UserGroups)
                .HasForeignKey(bc => bc.GroupId);
        }
        public DbSet<Address> Address { get; set; }        
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ActivityTrnsaction> ActivityTrnsaction { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        
    }
}
