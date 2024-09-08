namespace MGM_Lite.DTO.PurchaseDTO
{
    public class PurchaseCommonDTO
    {
        public PurchaseHeaderDTO header { get; set; }
        public List<PurchaseRowDTO> rows { get; set; }
    }
    public class PurchaseHeaderDTO
    {
        public long PurchaseReceiveId { get; set; }
        public string ReceiveCode { get; set; } = null!;
        public DateTime ReceivedDate { get; set; }
        public string ReceivedDateLanding { get; set; }
        public long AccountId { get; set; }
        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public long ActionById { get; set; }
        public string ActionByName { get; set; }
        public bool IsActive { get; set; }
    }
    public class PurchaseRowDTO
    {
        public long RowId { get; set; }
        public long HeaderId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; } 
        public string UomName { get; set; }  
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
