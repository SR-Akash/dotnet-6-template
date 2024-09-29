using MGM_Lite.DTO;
using MGM_Lite.DTO.ConfigurationDTO;

namespace MGM_Lite.IRepository
{
    public interface IConfigurationModule
    {
        Task<object> GetItemList(long accountId);
        Task<MessageHelper> CreateItem(ItemsDTO create);

        Task<MessageHelper> CreatePartner(PartnerDTO create);
        Task<List<PartnerDTO>> GetPartnerList(long accountId, long partnerTypeId);

        Task<object> GetBankName();
        Task<object> GetBankBranch(long bankId);
        Task<object> GetBankAccountType();
        Task<MessageHelper> CreateBankAccount(BankAccountDTO obj);
        Task<List<BankAccountDTO>> GetBankAccountLandingPagination(long accountId, long branchId);
    }
}
