using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.Account;

namespace JoinsPay_BackService.ModelConfiguration.Register.Account
{
    public class AccountConfiguration
    {
        public AccountConfiguration(EntityTypeBuilder<AccountDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Account").HasKey(t => t.id);

                entity.Property(t => t.code).HasMaxLength(10).IsRequired();

                entity.Property(t => t.name).HasMaxLength(50).IsRequired();
                
                entity.Property(t => t.agency).HasMaxLength(10).IsRequired();
                
                entity.Property(t => t.accountNumber).HasMaxLength(10).IsRequired();

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);

                //FK - Account Category
                entity.HasOne(t => t.AccountCategory).WithMany(t => t.Account).HasForeignKey(t => t.idAccountCategory).HasPrincipalKey(t => t.id);



            }
        }
    }
}
