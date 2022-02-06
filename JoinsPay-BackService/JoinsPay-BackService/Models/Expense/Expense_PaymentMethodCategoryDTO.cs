using JoinsPay_BackService.Models.Register.PaymentMethod;
using System;

namespace JoinsPay_BackService.Models.Expense
{
    public class Expense_PaymentMethodCategoryDTO
    {
        public long id { get; set; }
        public long idExpense { get; set; }
        public long idPaymentMethodCategory { get; set; }

        public DateTime dateCreated { get; set; }

        public ExpenseDTO expense { get; set; }
        public PaymentMethodCategoryDTO paymentMethodCategory { get; set; }
    }


}
