using MGM_Lite.DBContext;
using MGM_Lite.DTO;
using MGM_Lite.DTO.PurchaseDTO;
using MGM_Lite.IRepository;
#pragma warning disable

namespace MGM_Lite.Repository
{
    public class Purchase : IPurchase
    {
        private readonly AppDbContext _context;
        public Purchase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreatePurchaseReceive(PurchaseCommonDTO create)
        {
            try
            {
                Random rnd = new Random();

                var receiveCode = rnd.Next();
                var headerData = new Models.PurchaseReceiveHeader
                {
                    ReceiveCode = receiveCode.ToString(),
                    TotalAmount = create.rows.Sum(x => x.TotalAmount),
                    ActionById = create.header.ActionById,
                    IsActive = true,
                    ReceivedDate = create.header.ReceivedDate,
                    Remarks = create.header.Remarks,
                    SupplierId = create.header.SupplierId,
                    SupplierName = create.header.SupplierName,
                    TotalQty = create.rows.Sum(x => x.Quantity),
                    AccountId = create.header.AccountId
                };
                await _context.PurchaseReceiveHeaders.AddAsync(headerData);
                await _context.SaveChangesAsync();

                var rowsData = create.rows.Select(x => new Models.PurchaseReceiveRow
                {
                    HeaderId = headerData.PurchaseReceiveId,
                    IsActive = true,
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    TotalAmount = x.TotalAmount
                }).ToList();

                await _context.PurchaseReceiveRows.AddRangeAsync(rowsData);
                await _context.SaveChangesAsync();

                return new MessageHelper
                {
                    message = "Created successfully",
                    statusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PurchaseHeaderDTO>> GetPurchaseReceivedList(long accountId)
        {
            try
            {
                var data = (from h in _context.PurchaseReceiveHeaders
                            join p in _context.Partners on h.SupplierId equals p.PartnerId
                            join u in _context.Users on h.ActionById equals u.UserId
                            where h.AccountId == accountId && h.IsActive == true
                            select new PurchaseHeaderDTO
                            {
                                IsActive = h.IsActive,
                                AccountId = h.AccountId,
                                ActionById = h.ActionById,
                                ActionByName = u.UserName,
                                PurchaseReceiveId = h.PurchaseReceiveId,
                                ReceivedDateLanding = h.ReceivedDate.ToString("dd-MM-yyyy"),
                                ReceiveCode = h.ReceiveCode,
                                ReceivedDate = h.ReceivedDate,
                                Remarks = h.Remarks,
                                SupplierId = h.SupplierId,
                                SupplierName = h.SupplierName,
                                TotalAmount = h.TotalAmount,
                                TotalQty = h.TotalQty,
                                Address = p.Address
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
