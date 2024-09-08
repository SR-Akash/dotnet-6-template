using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class Partner
    {
        public long PartnerId { get; set; }
        public string PartnerName { get; set; } = null!;
        public long AccountId { get; set; }
        public long PartnerTypeId { get; set; }
        public string PartnerTypeName { get; set; } = null!;
        public string PartnerCode { get; set; } = null!;
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public long ActionById { get; set; }
        public bool IsActive { get; set; }
    }
}
