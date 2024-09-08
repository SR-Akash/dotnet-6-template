using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class PurchaseReceiveHeader
    {
        public long PurchaseReceiveId { get; set; }
        public long AccountId { get; set; }
        public string ReceiveCode { get; set; } = null!;
        public DateTime ReceivedDate { get; set; }
        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public long ActionById { get; set; }
        public bool IsActive { get; set; }
    }
}
