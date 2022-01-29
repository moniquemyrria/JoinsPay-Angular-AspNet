using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.Account;

namespace JoinsPay_BackService.ModelConfiguration.Register.Account
{
    public class AccountCategoryConfiguration
    {
        public AccountCategoryConfiguration(EntityTypeBuilder<AccountCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Account_Category").HasKey(t => t.id);

                entity.Property(t => t.initials).HasMaxLength(6).IsRequired();

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();
                
                entity.Property(t => t.standard).HasMaxLength(1).IsRequired();

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
