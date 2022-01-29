using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JoinsPay_BackService.Models.Register.Department;

namespace JoinsPay_BackService.ModelConfiguration.Register.Departament
{
    public class DepartmentConfiguration
    {
        public DepartmentConfiguration(EntityTypeBuilder<DepartmentDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Register.Department").HasKey(t => t.id);

                entity.Property(t => t.name).HasMaxLength(50).IsRequired();
                

                entity.Property(t => t.deleted).HasMaxLength(1);

                entity.Property(t => t.dateCreated);

                //FK - Departament Category
                entity.HasOne(t => t.departmentCategory).WithMany(t => t.Departments).HasForeignKey(t => t.idDepartamentCategory).HasPrincipalKey(t => t.id);



            }
        }
    }
}
