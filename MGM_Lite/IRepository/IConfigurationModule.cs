using MGM_Lite.DTO;
using MGM_Lite.DTO.ConfigurationDTO;

namespace MGM_Lite.IRepository
{
    public interface IConfigurationModule
    {
        Task<object> GetItemList(long accountId);
        Task<MessageHelper> CreateItem(ItemsDTO create);
    }
}
