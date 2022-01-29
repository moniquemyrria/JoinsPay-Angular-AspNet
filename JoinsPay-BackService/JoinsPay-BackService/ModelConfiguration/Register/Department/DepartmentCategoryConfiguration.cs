using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.Department;

namespace JoinsPay_BackService.ModelConfiguration.Register.Departament
{
    public class DepartmentCategoryConfiguration
    {
        public DepartmentCategoryConfiguration(EntityTypeBuilder<DepartmentCategoryDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Department_Category").HasKey(t => t.id);

                entity.Property(t => t.description).HasMaxLength(30).IsRequired();

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);


            }
        }
    }
}
