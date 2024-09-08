using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class PurchaseReceiveRow
    {
        public long RowId { get; set; }
        public long HeaderId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
