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
                if (string.IsNullOrWhiteSpace(create.ItemName))
                {
                    throw new Exception("Invalid item name.");
                }

                var exist = _context.Items.Where(x => x.AccountId == create.AccountId && x.ItemName == create.ItemName && x.IsActive == true).Select(x => x).FirstOrDefault();
                if (exist != null)
                {
                    throw new Exception("Item already exist");
                }
                Random rnd = new Random();
                int num = rnd.Next();

                var item = new Models.Item
                {
                    AccountId = (create.AccountId == null || create.AccountId == 0) ? 1 : create.AccountId,
                    ActionById = create.ActionById,
                    IsActive = true,
                    ItemCode = "ITM-" + num.ToString(),
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

        public async Task<MessageHelper> CreatePartner(PartnerDTO create)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(create.PartnerName))
                {
                    throw new Exception("Invalid partner name.");
                }

                var exist = _context.Partners.Where(x => x.AccountId == create.AccountId && x.PartnerName == create.PartnerName && x.PartnerTypeId == create.PartnerTypeId
                && x.IsActive == true).Select(x => x).FirstOrDefault();
                if (exist != null)
                {
                    throw new Exception("Partner already exist");
                }
                Random rnd = new Random();
                int num = rnd.Next();

                var Partner = new Models.Partner
                {
                    AccountId = (create.AccountId == null || create.AccountId == 0) ? 1 : create.AccountId,
                    ActionById = create.ActionById,
                    IsActive = true,
                    PartnerCode = "PRT-" + num.ToString(),
                    Address = create.Address,
                    PartnerName = create.PartnerName,
                    PartnerTypeId = create.PartnerTypeId,
                    Mobile = create.Mobile,
                    PartnerTypeName = create.PartnerTypeName,

                };
                await _context.Partners.AddAsync(Partner);
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

        public async Task<List<PartnerDTO>> GetPartnerList(long accountId, long partnerTypeId)
        {
            var data = _context.Partners.Where(x => x.AccountId == accountId && x.PartnerTypeId==partnerTypeId).Select(x => new PartnerDTO
            {
                PartnerId = x.PartnerId,
                PartnerName = x.PartnerName,
                PartnerCode = x.PartnerCode,
                Address = x.Address,
                Mobile = x.Mobile
            }).ToList();

            return data;
        }
    }
}
