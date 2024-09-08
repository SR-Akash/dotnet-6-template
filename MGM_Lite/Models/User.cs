using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool IsMasterUser { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Mobile { get; set; }
        public long AccountId { get; set; }
    }
}
