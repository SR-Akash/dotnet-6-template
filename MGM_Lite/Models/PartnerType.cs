using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class PartnerType
    {
        public long PartnerTypeId { get; set; }
        public string PartnerTypeName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
