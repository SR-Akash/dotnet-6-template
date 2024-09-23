using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class SubLedgerRow
    {
        public long RowId { get; set; }
        public long SubLedgerHeaderId { get; set; }
        public long PartnerId { get; set; }
        public long ChartOfAccId { get; set; }
        public string ChartOfAccName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
