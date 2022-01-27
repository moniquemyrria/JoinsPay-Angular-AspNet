using JoinsPay_BackService.ModelConfiguration.Register.ExpenseCategory;
using JoinsPay_BackService.ModelConfiguration.Register.RevenueCategory;
using JoinsPay_BackService.Models.Register.ExpenseCategory;
using JoinsPay_BackService.Models.Register.RevenueCategory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinsPay_BackService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (modelBuilder != null)
            {
                base.OnModelCreating(modelBuilder);
                _ = new RevenueCategoryConfiguration(modelBuilder.Entity<RevenueCategoryDTO>());
                
                _ = new ExpenseCategoryConfiguration(modelBuilder.Entity<ExpenseCategoryDTO>());

            }
        }

        public DbSet<RevenueCategoryDTO> RevenueCategories { get; set; }

        public DbSet<ExpenseCategoryDTO> ExpenseCategories { get; set; }
    }
}
