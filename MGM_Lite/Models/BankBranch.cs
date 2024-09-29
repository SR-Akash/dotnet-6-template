using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class BankBranch
    {
        public long BankBranchId { get; set; }
        public string BankBranchCode { get; set; } = null!;
        public string BankBranchName { get; set; } = null!;
        public string District { get; set; } = null!;
        public long BankId { get; set; }
        public string BankName { get; set; } = null!;
        public string BankShortName { get; set; } = null!;
        public string BankCode { get; set; } = null!;
        public string RoutingNo { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
