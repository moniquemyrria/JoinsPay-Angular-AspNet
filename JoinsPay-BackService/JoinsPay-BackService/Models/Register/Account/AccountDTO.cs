using System;

namespace JoinsPay_BackService.Models.Register.Account
{
    public class AccountDTO
    {
        public long id { get; set; }
        public long idAccountCategory { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string agency { get; set; }
        public string accountNumber { get; set; }
        public string deleted { get; set; }
        public DateTime dateCreated { get; set; }

        public AccountCategoryDTO AccountCategory { get; set; }
    }


}
