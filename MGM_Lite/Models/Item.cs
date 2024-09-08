using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class Item
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public long? UomId { get; set; }
        public string UomName { get; set; } = null!;
        public string? ItemDescription { get; set; }
        public bool IsActive { get; set; }
        public long ActionById { get; set; }
        public long AccountId { get; set; }
    }
}
