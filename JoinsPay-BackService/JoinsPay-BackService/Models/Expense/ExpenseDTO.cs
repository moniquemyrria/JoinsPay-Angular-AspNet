
using JoinsPay_BackService.Models.Register.Account;
using JoinsPay_BackService.Models.Register.Department;
using JoinsPay_BackService.Models.Register.ExpenseCategory;
using JoinsPay_BackService.Models.Register.PaymentMethod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Expense
{
    public class ExpenseDTO
    {
        public long id { get; set; }
        public long idExpenseCategory { get; set; }
        public long idPaymentMethod { get; set; }
        public long idDepartment { get; set; }
        public long idAccount { get; set; }
        public long idExpenseStatus { get; set; }
        public long idExpenseType { get; set; }


        public Double amount { get; set; } //valor
        public Double fine { get; set; } //multa
        public Double interest { get; set; } //juros
        public Double discount { get; set; } //desconto
        public int qtyInstallment { get; set; } //quantidade de parcelas
        public int installment { get; set; } //parcela
        
        public string description { get; set; }

        public DateTime dateCreated { get; set; }
        public DateTime? dueDate { get; set; } //vencimento / primeiro vencimento
        public DateTime? paymentDate { get; set; } //data de pagamento

        public ExpenseCategoryDTO expenseCategory { get; set; }
        public PaymentMethodDTO paymentMethod { get; set; }
        public DepartmentDTO department { get; set; }
        public AccountDTO account { get; set; }
        public ExpenseStatusDTO expenseStatus { get; set; }
        public ExpenseTypeDTO expenseType { get; set; }

        [JsonIgnore]
        public List<Expense_PaymentMethodCategoryDTO> expensePaymentMethodCategories { get; set; }

        [NotMapped]
        public List<PaymentMethodCategoryDTO> paymentMethodCategory { get; set; }

    }


}
