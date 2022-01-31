using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.Revenue;

namespace JoinsPay_BackService.ModelConfiguration.Revenue
{
    public class RevenueConfiguration
    {
        public RevenueConfiguration(EntityTypeBuilder<RevenueDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Revenue").HasKey(t => t.id);

                entity.Property(t => t.amount).IsRequired();
                
                entity.Property(t => t.description).HasMaxLength(200);
                
                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);

                //FK - Renvenue Category
                entity.HasOne(t => t.revenueCategory).WithMany(t => t.Income).HasForeignKey(t => t.idRevenueCategory).HasPrincipalKey(t => t.id);

                //FK - account
                entity.HasOne(t => t.account).WithMany(t => t.Income).HasForeignKey(t => t.idAccount).HasPrincipalKey(t => t.id);

                //FK - department
                entity.HasOne(t => t.department).WithMany(t => t.Income).HasForeignKey(t => t.idDepartment).HasPrincipalKey(t => t.id);


            }
        }
    }
}
