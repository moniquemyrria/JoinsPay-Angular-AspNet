using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JoinsPay_BackService.Models.Register.Account
{
    public class AccountCategoryDTO
    {
        public long id { get; set; }
        public string initials { get; set; }
        public string description { get; set; }
        public string deleted { get; set; }
        public string standard { get; set; }
        public DateTime dateCreated { get; set; }

        [JsonIgnore]
        public List<AccountDTO> Account { get; set; }
    }


}
