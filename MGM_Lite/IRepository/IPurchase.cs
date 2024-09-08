using MGM_Lite.DTO;
using MGM_Lite.DTO.PurchaseDTO;

namespace MGM_Lite.IRepository
{
    public interface IPurchase
    {
        Task<MessageHelper> CreatePurchaseReceive(PurchaseCommonDTO create);
        Task<List<PurchaseHeaderDTO>> GetPurchaseReceivedList(long accountId);
    }
}
