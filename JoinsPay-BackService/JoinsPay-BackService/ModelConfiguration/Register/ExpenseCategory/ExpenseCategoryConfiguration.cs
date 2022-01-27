using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.ExpenseCategory;

namespace JoinsPay_BackService.ModelConfiguration.Register.ExpenseCategory
{
    public class ExpenseCategoryConfiguration
    {
        public ExpenseCategoryConfiguration(EntityTypeBuilder<ExpenseCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Expense_Category").HasKey(t => t.id);

                entity.Property(t => t.initials).HasMaxLength(6).IsRequired();

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();

                entity.Property(t => t.color).HasMaxLength(10);

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
