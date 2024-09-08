using MGM_Lite.DBContext;
using MGM_Lite.DTO;
using MGM_Lite.DTO.AuthDTO;
using MGM_Lite.DTO.ConfigurationDTO;
using MGM_Lite.IRepository;
using Microsoft.Extensions.Options;

namespace MGM_Lite.Repository
{
    public class ConfigurationModule : IConfigurationModule
    {
        public readonly AppDbContext _context;
        public ConfigurationModule(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateItem(ItemsDTO create)
        {
            try
            {
                var exist = _context.Items.Where(x => x.AccountId == create.AccountId && x.ItemName == create.ItemName && x.IsActive == true).Select(x => x).FirstOrDefault();
                if (exist != null)
                {
                    throw new Exception("Item already exist");
                }

                var item = new Models.Item
                {
                    AccountId = (create.AccountId == null || create.AccountId == 0) ? 1 : create.AccountId,
                    ActionById = create.ActionById,
                    IsActive = true,
                    ItemCode = create.ItemCode,
                    ItemDescription = create.ItemDescription,
                    ItemName = create.ItemName,
                    UomId = 0,
                    UomName = create.UomName,

                };
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    message = "Created successfully",
                    statusCode = 200,
                };

            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<object> GetItemList(long accountId)
        {
            var data = _context.Items.Where(x => x.AccountId == accountId).Select(x => new
            {
                ItemId = x.ItemId,
                ItemName = x.ItemName,
                ItemCode = x.ItemCode,
                UomName = x.UomName,
                ItemDescription = x.ItemDescription
            }).ToList();
            return data;
        }
    }
}
