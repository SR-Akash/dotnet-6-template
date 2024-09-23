namespace MGM_Lite.DTO
{
    public class ChartOfAccCategoryDTO
    {
        public long ChartOfAccCategoryId { get; set; }
        public string ChartOfAccCategoryName { get; set; } = null!;
        public long AccountId { get; set; }
        public long ActionById { get; set; }
    }
    public class ChartofAccDTO
    {
        public long ChartofAccId { get; set; }
        public string ChartOfAccName { get; set; } = null!;
        public string ChartOfAccCode { get; set; } = null!;
        public long AccountId { get; set; }
        public long BranchId { get; set; }
        public long? ChartOfAccCategoryId { get; set; }
        public string? ChartOfAccCategoryName { get; set; }
        public bool IsActive { get; set; }
        public long ActionById { get; set; }
        public string? ActionByName { get; set; }
        public long TemplateId { get; set; }
    }

    public class ChartofAccTemplateDTO
    {
        public long BranchId { get; set; }
        public long TemplateId { get; set; }
        public string ChartOfAccName { get; set; } = null!;
        public string ChartOfAccCode { get; set; } = null!;
        public long AccountId { get; set; }
        public long? ChartOfAccCategoryId { get; set; }
        public string? ChartOfAccCategoryName { get; set; }
        public bool IsPreviouslyChecked { get; set; }
        public long ActionById { get; set; }
    }

    public class ChartofAccCategoryDTO
    {
        public long ChartOfAccCategoryId { get; set; }
        public string ChartOfAccCategoryName { get; set; }
    }



    public class JournalVoucherLandingPaginationDTO
    {
        public List<JournalVoucherHeaderDTO> data { get; set; }
        public long CurrentPage { get; set; }
        public long TotalCount { get; set; }
        public long PageSize { get; set; }
    }

    public class JournalVoucherHeaderDTO
    {
        public long Sl { get; set; }
        public long SubLedgerHeaderId { get; set; }
        public string SubLedgerCode { get; set; }
        public long AccountId { get; set; }
        public long BranchId { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
        public long TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public long TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public string InstrumentNo { get; set; }
        public string InstrumentType { get; set; }
        public long ActionById { get; set; }
        public string ActionByName { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class JournalVoucherRowDTO
    {
        public long RowId { get; set; }
        public long SubLedgerHeaderId { get; set; }
        public long PartnerId { get; set; }
        public string PartnerName { get; set; }
        public long ChartOfAccId { get; set; }
        public string ChartOfAccName { get; set; } = null!;
        public string ChartOfAccCode { get; set; } = null!;
        public decimal Amount { get; set; }
    }

    public class JournalVoucherCommonDTO
    {
        public JournalVoucherHeaderDTO header { get; set; }
        public List<JournalVoucherRowDTO> rows { get; set; }
    }
}
