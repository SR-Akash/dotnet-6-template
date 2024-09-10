using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class ChartOfAccCategory
    {
        public long ChartOfAccCategoryId { get; set; }
        public string ChartOfAccCategoryName { get; set; } = null!;
        public long AccountId { get; set; }
        public long ActionById { get; set; }
        public bool IsActive { get; set; }
    }
}
