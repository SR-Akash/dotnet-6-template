using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class Bank
    {
        public long BankId { get; set; }
        public string BankName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string BankCode { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
