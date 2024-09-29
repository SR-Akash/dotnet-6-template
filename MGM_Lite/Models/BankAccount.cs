using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class BankAccount
    {
        public long BankAccountId { get; set; }
        public long AccountId { get; set; }
        public long BranchId { get; set; }
        public long ChartofAccId { get; set; }
        public long BankId { get; set; }
        public string BankShortCode { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string RoutingNumber { get; set; } = null!;
        public long BankBranchId { get; set; }
        public string BankBranchName { get; set; } = null!;
        public string BankAccHolderName { get; set; } = null!;
        public string BankAccountNumber { get; set; } = null!;
        public long BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; } = null!;
        public long ActionById { get; set; }
        public DateTime LastActionDatetime { get; set; }
    }
}
