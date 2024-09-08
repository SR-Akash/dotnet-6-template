using System;
using System.Collections.Generic;

namespace MGM_Lite.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? Designation { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? Religion { get; set; }
        public DateTime? JoinDate { get; set; }
        public bool IsActive { get; set; }
        public string? PresentAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long ActionById { get; set; }
        public long AccountId { get; set; }
    }
}
