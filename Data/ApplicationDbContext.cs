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

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    //modelBuilder.Entity<UserAccountGroup>()
//    //    .HasKey(uag => new { uag.UserId, uag.GroupId });  
//    //modelBuilder.Entity<UserAccountGroup>()
//    //    .HasOne(uag => uag.UserAccount)
//    //    .WithMany(ua => ua.UserAccountGroups)
//    //    .HasForeignKey(uag => uag.UserId);  
//    //modelBuilder.Entity<UserAccountGroup>()
//    //    .HasOne(uag => uag.Group)
//    //    .WithMany(g => g.UserAccountGroups)
//    //    .HasForeignKey(uag => uag.GroupId);
//}

        public DbSet<Authentication.Models.Address> Address { get; set; }
        public DbSet<UserAccountGroup> UserAccountGroups { get; set; }
        public DbSet<Authentication.Models.Payment> Payment { get; set; }
        public DbSet<Authentication.Models.UserAccount> UserAccount { get; set; }
        public DbSet<Authentication.Models.Wallet> Wallet { get; set; }
        public DbSet<Authentication.Models.Activity> Activity { get; set; }
       // public DbSet<UserGroup> UserGroups { get; set; }
    }
}
