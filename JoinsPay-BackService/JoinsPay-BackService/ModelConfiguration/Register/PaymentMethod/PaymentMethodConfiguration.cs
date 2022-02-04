using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.PaymentMethod;

namespace JoinsPay_BackService.ModelConfiguration.Register.PaymentMethod
{
    public class PaymentMethodConfiguration
    {
        public PaymentMethodConfiguration(EntityTypeBuilder<PaymentMethodDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Payment_Method").HasKey(t => t.id);

                entity.Property(t => t.name).HasMaxLength(30).IsRequired();

                entity.Property(t => t.acceptInstallment).IsRequired();
                
                entity.Property(t => t.numberInstallments);
                
                entity.Property(t => t.intervalDaysInstallments);

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);

                //FK - Account 
                entity.HasOne(t => t.account).WithMany(t => t.PaymentMethods).HasForeignKey(t => t.idAccount).HasPrincipalKey(t => t.id);

               

            }
        }
    }
}
