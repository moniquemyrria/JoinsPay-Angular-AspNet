using JoinsPay_BackService.Models.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoinsPay_BackService.ModelConfiguration.Expense
{
    public class ExpenseStatusConfiguration
    {
        public ExpenseStatusConfiguration(EntityTypeBuilder<ExpenseStatusDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Expense.Expense_Status").HasKey(t => t.id);

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
