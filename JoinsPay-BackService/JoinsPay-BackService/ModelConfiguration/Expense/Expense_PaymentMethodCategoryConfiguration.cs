using JoinsPay_BackService.Models.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoinsPay_BackService.ModelConfiguration.Expense
{
    public class Expense_PaymentMethodCategoryConfiguration
    {
        public Expense_PaymentMethodCategoryConfiguration(EntityTypeBuilder<Expense_PaymentMethodCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Expense.Expense_Payment_Method_Category").HasKey(t => t.id);

                entity.Property(t => t.dateCreated);

                //FK - Expense
                entity.HasOne(t => t.expense).WithMany(t => t.expensePaymentMethodCategories).HasForeignKey(t => t.idExpense).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);


                //FK - Payment Method Category 
                entity.HasOne(t => t.paymentMethodCategory).WithMany(t => t.expensePaymentMethodCategories).HasForeignKey(t => t.idPaymentMethodCategory).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

            }
        }
    }
}
