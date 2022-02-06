using JoinsPay_BackService.ModelConfiguration.Register.ExpenseCategory;
using JoinsPay_BackService.ModelConfiguration.Register.RevenueCategory;
using JoinsPay_BackService.ModelConfiguration.Register.Account;
using JoinsPay_BackService.ModelConfiguration.Register.Departament;
using JoinsPay_BackService.Models.Register.ExpenseCategory;
using JoinsPay_BackService.Models.Register.RevenueCategory;
using JoinsPay_BackService.Models.Register.Account;
using JoinsPay_BackService.Models.Register.Department;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JoinsPay_BackService.ModelConfiguration.Revenue;
using JoinsPay_BackService.Models.Register.Revenue;
using JoinsPay_BackService.Models.Register.PaymentMethod;
using JoinsPay_BackService.ModelConfiguration.Register.PaymentMethod;
using JoinsPay_BackService.ModelConfiguration.Register.PaymentMethodCategory;
using JoinsPay_BackService.ModelConfiguration.Expense;
using JoinsPay_BackService.Models.Expense;

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

                _ = new AccountCategoryConfiguration(modelBuilder.Entity<AccountCategoryDTO>());

                _ = new AccountConfiguration(modelBuilder.Entity<AccountDTO>());
                
                _ = new DepartmentCategoryConfiguration(modelBuilder.Entity<DepartmentCategoryDTO>());
                
                _ = new DepartmentConfiguration(modelBuilder.Entity<DepartmentDTO>());

                _ = new RevenueConfiguration(modelBuilder.Entity<RevenueDTO>());
                
                _ = new PaymentMethodCategoryConfiguration(modelBuilder.Entity<PaymentMethodCategoryDTO>());
                
                _ = new PaymentMethodConfiguration(modelBuilder.Entity<PaymentMethodDTO>());
                
                _ = new PaymentMethod_PaymentMethodCategoryConfiguration(modelBuilder.Entity<PaymentMethod_PaymentMethodCategoryDTO>());
                
                _ = new ExpenseStatusConfiguration(modelBuilder.Entity<ExpenseStatusDTO>());
                
                _ = new ExpenseTypeConfiguration(modelBuilder.Entity<ExpenseTypeDTO>());
                
                _ = new ExpenseConfiguration(modelBuilder.Entity<ExpenseDTO>());
                
                _ = new Expense_PaymentMethodCategoryConfiguration(modelBuilder.Entity<Expense_PaymentMethodCategoryDTO>());

            }
        }

        public DbSet<RevenueCategoryDTO> RevenueCategories { get; set; }

        public DbSet<ExpenseCategoryDTO> ExpenseCategories { get; set; }
        
        public DbSet<AccountCategoryDTO> AccountCategories { get; set; }
        
        public DbSet<AccountDTO> Accounts { get; set; }
        
        public DbSet<DepartmentCategoryDTO> DepartmentCategories { get; set; }
        
        public DbSet<DepartmentDTO> Departments { get; set; }
        
        public DbSet<RevenueDTO> Incomes { get; set; }

        public DbSet<PaymentMethodCategoryDTO> PaymentMethodCategories { get; set; }

        public DbSet<PaymentMethodDTO> PaymentMethods { get; set; }
        
        public DbSet<PaymentMethod_PaymentMethodCategoryDTO> PaymentMethodsPaymentMethodCategories { get; set; }

        public DbSet<ExpenseStatusDTO> ExpenseStatus { get; set; }
        
        public DbSet<ExpenseTypeDTO> ExpenseType { get; set; }
        
        public DbSet<ExpenseDTO> Expenses { get; set; }

        public DbSet<Expense_PaymentMethodCategoryDTO> ExpensePaymentMethodCategories { get; set; }

    }
}
