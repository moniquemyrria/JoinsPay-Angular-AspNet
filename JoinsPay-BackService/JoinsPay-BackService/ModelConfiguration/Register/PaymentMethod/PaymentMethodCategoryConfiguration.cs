using JoinsPay_BackService.Models.Register.PaymentMethod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoinsPay_BackService.ModelConfiguration.Register.PaymentMethodCategory
{
    public class PaymentMethodCategoryConfiguration
    {
        public PaymentMethodCategoryConfiguration(EntityTypeBuilder<PaymentMethodCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Payment_Method_Category").HasKey(t => t.id);

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
