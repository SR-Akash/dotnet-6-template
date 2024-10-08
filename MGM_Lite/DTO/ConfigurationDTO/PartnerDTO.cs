﻿namespace MGM_Lite.DTO.ConfigurationDTO
{
    public class PartnerDTO
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

    public class BankAccountDTO
    {
        public long BankAccountId { get; set; }
        public long AccountId { get; set; }
        public long BranchId { get; set; }
        public long ChartofAccId { get; set; }
        public long BankId { get; set; }
        public string BankShortCode { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string RoutingNumber { get; set; } = null!;
        public long BankBranchId { get; set; }
        public string BankBranchName { get; set; } = null!;
        public string BankAccHolderName { get; set; } = null!;
        public string BankAccountNumber { get; set; } = null!;
        public long BankAccountTypeId { get; set; }
        public string BankAccountTypeName { get; set; } = null!;
        public long ActionById { get; set; }
    }
}
