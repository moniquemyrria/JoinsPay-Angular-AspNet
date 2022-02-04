
using JoinsPay_BackService.Models.Register.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Register.PaymentMethod
{
    public class PaymentMethodDTO
    {
        public long id { get; set; }
        public long idAccount { get; set; }
        public string name { get; set; }
        public Boolean acceptInstallment { get; set; }
        public int numberInstallments { get; set; }
        public int intervalDaysInstallments { get; set; }
        public string deleted { get; set; }

        public DateTime dateCreated { get; set; }

        public AccountDTO account { get; set; }

        public List<PaymentMethod_PaymentMethodCategoryDTO> paymentMethodsPaymentMethodCategories { get; set; }

        [NotMapped]
        public List<PaymentMethodCategoryDTO> paymentMethodCategory { get; set; }
    }


}
