
using System;

namespace JoinsPay_BackService.Models.Register.Department
{
    public class DepartmentDTO
    {
        public long id { get; set; }
        public long idDepartamentCategory { get; set; }
        public string name { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public DepartmentCategoryDTO departmentCategory { get; set; }

    }


}
