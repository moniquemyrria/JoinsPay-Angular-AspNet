using JoinsPay_BackService.Models.Register.Account;
using JoinsPay_BackService.Models.Register.Department;
using JoinsPay_BackService.Models.Register.RevenueCategory;
using System;

namespace JoinsPay_BackService.Models.Register.Revenue
{
    public class RevenueDTO
    {
        public long id { get; set; }
        public long idRevenueCategory { get; set; }
        public long idAccount { get; set; }
        public long idDepartment { get; set; }
        public Double amount { get; set; }
        public string description { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public RevenueCategoryDTO revenueCategory { get; set; }
        
        public AccountDTO account { get; set; }
        
        public DepartmentDTO department { get; set; }


    }


}
