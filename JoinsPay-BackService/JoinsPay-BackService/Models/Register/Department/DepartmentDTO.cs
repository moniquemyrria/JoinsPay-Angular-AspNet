
using JoinsPay_BackService.Models.Register.Account;
using JoinsPay_BackService.Models.Expense;
using JoinsPay_BackService.Models.Register.Revenue;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public List<RevenueDTO> Income { get; set; }

        [JsonIgnore]
        public List<AccountDTO> Account { get; set; }

        [JsonIgnore]
        public List<ExpenseDTO> Expenses { get; set; }


    }


}
