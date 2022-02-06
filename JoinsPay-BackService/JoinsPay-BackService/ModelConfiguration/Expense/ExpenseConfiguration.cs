using JoinsPay_BackService.Models.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoinsPay_BackService.ModelConfiguration.Expense
{
    public class ExpenseConfiguration
    {
        public ExpenseConfiguration(EntityTypeBuilder<ExpenseDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Expense").HasKey(t => t.id);

                entity.Property(t => t.amount).IsRequired();
                
                entity.Property(t => t.fine);
                
                entity.Property(t => t.interest);
                
                entity.Property(t => t.discount);
                
                entity.Property(t => t.qtyInstallment);
                
                entity.Property(t => t.installment);

                entity.Property(t => t.description).HasMaxLength(200);

                entity.Property(t => t.dateCreated);
                
                entity.Property(t => t.dueDate);

                entity.Property(t => t.paymentDate);

               
                //FK - Expense Category
                entity.HasOne(t => t.expenseCategory).WithMany(t => t.Expenses).HasForeignKey(t => t.idExpenseCategory).HasPrincipalKey(t => t.id) ;

                //FK - Payment method
                entity.HasOne(t => t.paymentMethod).WithMany(t => t.Expenses).HasForeignKey(t => t.idPaymentMethod).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

                //FK - Department
                entity.HasOne(t => t.department).WithMany(t => t.Expenses).HasForeignKey(t => t.idDepartment).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

                //FK - Department
                entity.HasOne(t => t.account).WithMany(t => t.Expenses).HasForeignKey(t => t.idAccount).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

                //FK - Expense Status
                entity.HasOne(t => t.expenseStatus).WithMany(t => t.Expenses).HasForeignKey(t => t.idExpenseStatus).HasPrincipalKey(t => t.id);

                //FK - Expense Type
                entity.HasOne(t => t.expenseType).WithMany(t => t.Expenses).HasForeignKey(t => t.idExpenseType).HasPrincipalKey(t => t.id);

            }
        }
    }
}
