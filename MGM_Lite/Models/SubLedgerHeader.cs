using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class SubLedgerHeader
    {
        public long SubLedgerHeaderId { get; set; }
        public string SubLedgerCode { get; set; } = null!;
        public long AccountId { get; set; }
        public long BranchId { get; set; }
        public string Narration { get; set; } = null!;
        public string? ManualNarration { get; set; }
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public long ExtraTransactionId { get; set; }
        public string? ExtraTransactionCode { get; set; }
        public long TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; } = null!;
        public long BankAccId { get; set; }
        public string? BankAccNumber { get; set; }
        public string? InstrumentNo { get; set; }
        public string? InstrumentType { get; set; }
        public long ActionById { get; set; }
        public string ActionByName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime ActionDatetime { get; set; }
        public string? Attachment { get; set; }
    }
}
