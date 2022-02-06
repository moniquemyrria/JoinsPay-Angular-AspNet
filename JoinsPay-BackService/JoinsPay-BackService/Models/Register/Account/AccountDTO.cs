using JoinsPay_BackService.Models.Register.Department;
using JoinsPay_BackService.Models.Expense;
using JoinsPay_BackService.Models.Register.PaymentMethod;
using JoinsPay_BackService.Models.Register.Revenue;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Register.Account
{
    public class AccountDTO
    {
        public long id { get; set; }
        public long idAccountCategory { get; set; }
        public long idDepartment { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string agency { get; set; }
        public string accountNumber { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public AccountCategoryDTO accountCategory { get; set; }

        public DepartmentDTO department { get; set; }

        [JsonIgnore]
        public List<RevenueDTO> Income { get; set; }

        [JsonIgnore]
        public List<PaymentMethodDTO> PaymentMethods { get; set; }

        [JsonIgnore]
        public List<ExpenseDTO> Expenses { get; set; }
    }


}
