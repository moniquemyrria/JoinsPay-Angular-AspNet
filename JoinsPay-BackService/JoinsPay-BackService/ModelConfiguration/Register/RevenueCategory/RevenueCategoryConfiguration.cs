using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.RevenueCategory;

namespace JoinsPay_BackService.ModelConfiguration.Register.RevenueCategory
{
    public class RevenueCategoryConfiguration
    {
        public RevenueCategoryConfiguration(EntityTypeBuilder<RevenueCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Revenue_Category").HasKey(t => t.id);

                entity.Property(t => t.initials).HasMaxLength(6).IsRequired();

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();

                entity.Property(t => t.color).HasMaxLength(10);

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
