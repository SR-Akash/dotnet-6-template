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
}
