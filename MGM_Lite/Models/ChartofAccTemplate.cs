using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class ChartofAccTemplate
    {
        public long TemplateId { get; set; }
        public string ChartOfAccName { get; set; } = null!;
        public string ChartOfAccCode { get; set; } = null!;
        public long AccountId { get; set; }
        public long ChartOfAccCategoryId { get; set; }
        public string ChartOfAccCategoryName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
