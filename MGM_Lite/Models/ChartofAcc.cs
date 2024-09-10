using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class ChartofAcc
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
        public DateTime LastActionDateTime { get; set; }
        public long TemplateId { get; set; }
    }
}
