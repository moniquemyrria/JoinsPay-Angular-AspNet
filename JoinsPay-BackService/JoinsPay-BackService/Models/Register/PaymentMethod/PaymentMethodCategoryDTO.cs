using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Register.PaymentMethod
{
    public class PaymentMethodCategoryDTO
    {
        public long id { get; set; }
        public string description { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        [JsonIgnore]
        public List<PaymentMethod_PaymentMethodCategoryDTO> paymentMethodsPaymentMethodCategories { get; set; }
    }


}
