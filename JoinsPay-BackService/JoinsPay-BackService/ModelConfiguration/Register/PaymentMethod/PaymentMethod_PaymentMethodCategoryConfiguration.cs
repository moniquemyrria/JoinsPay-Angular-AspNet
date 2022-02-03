using JoinsPay_BackService.Models.Register.PaymentMethod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoinsPay_BackService.ModelConfiguration.Register.PaymentMethod
{
    public class PaymentMethod_PaymentMethodCategoryConfiguration
    {
        public PaymentMethod_PaymentMethodCategoryConfiguration(EntityTypeBuilder<PaymentMethod_PaymentMethodCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Payment_Method_Payment_Method_Category").HasKey(t => t.id);

                entity.Property(t => t.dateCreated);

                //FK - Payment Method Category 
                entity.HasOne(t => t.paymentMethodCategory).WithMany(t => t.paymentMethodsPaymentMethodCategories).HasForeignKey(t => t.idPaymentMethodCategory).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

                //FK - Payment Method 
                entity.HasOne(t => t.paymentMethod).WithMany(t => t.paymentMethodsPaymentMethodCategories).HasForeignKey(t => t.idPaymentMethod).HasPrincipalKey(t => t.id).OnDelete(DeleteBehavior.Restrict);

               

            }
        }
    }
}
