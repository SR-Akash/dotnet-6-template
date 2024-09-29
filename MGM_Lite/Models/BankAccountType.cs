using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class BankAccountType
    {
        public long BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
