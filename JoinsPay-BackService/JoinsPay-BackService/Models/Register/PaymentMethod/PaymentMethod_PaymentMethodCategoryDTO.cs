
using System;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Register.PaymentMethod
{
    public class PaymentMethod_PaymentMethodCategoryDTO
    {
        public long id { get; set; }
        public long idPaymentMethod { get; set; }
        public long idPaymentMethodCategory { get; set; }
        
        public DateTime dateCreated { get; set; }

        [JsonIgnore]
        public PaymentMethodDTO paymentMethod { get; set; }
        public PaymentMethodCategoryDTO paymentMethodCategory { get; set; }
       

    }


}
