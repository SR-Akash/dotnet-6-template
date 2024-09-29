using MGM_Lite.DBContext;
using MGM_Lite.DTO;
using MGM_Lite.DTO.AuthDTO;
using MGM_Lite.DTO.ConfigurationDTO;
using MGM_Lite.IRepository;
using MGM_Lite.Models;
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

        public async Task<MessageHelper> CreateBankAccount(BankAccountDTO obj)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = _context.BankAccounts.FirstOrDefault(x =>
                    x.BankId == obj.BankId && x.BankAccountNumber == obj.BankAccountNumber && x.AccountId == obj.AccountId
                    && x.BranchId == obj.BranchId);

                if (result != null)
                    throw new Exception("Bank Account Number Already Exist");


                var details = new BankAccount
                {
                    AccountId = obj.AccountId,
                    BranchId = obj.BranchId,
                    ActionById = obj.ActionById,
                    BankAccHolderName = obj.BankAccHolderName,
                    BankAccountId = obj.BankAccountId,
                    BankAccountNumber = obj.BankAccountNumber,
                    BankAccountTypeId = obj.BankAccountTypeId,
                    BankAccountTypeName = obj.BankAccountTypeName,
                    BankBranchId = obj.BankBranchId,
                    BankBranchName = obj.BankBranchName,
                    BankId = obj.BankId,
                    BankName = obj.BankName,
                    BankShortCode = obj.BankShortCode,
                    ChartofAccId = 0,
                    LastActionDatetime = DateTime.Now,
                    RoutingNumber = obj.RoutingNumber,
                };

                await _context.BankAccounts.AddAsync(details);
                await _context.SaveChangesAsync();

                var chartOfCode = await Task.FromResult((from a in _context.ChartofAccs
                                                         where a.AccountId == obj.AccountId
                                                         && a.BranchId == obj.BranchId
                                                         orderby a.ChartofAccId descending
                                                         select a.ChartOfAccCode).FirstOrDefault());

                if (obj.BankAccountTypeId != 8) // Bank Overdraft (OD) Account
                {
                    var code = Convert.ToInt32(chartOfCode) + 1;
                    var bankaccountTypeId = new List<long> { 3, 4 }; // Fixed Deposit Account,Recurring Deposit Account
                    var chartofAcc = new Models.ChartofAcc();
                    if (bankaccountTypeId.Contains(obj.BankAccountTypeId))
                    {
                        // var chartofacccategoryid = _context.ChartOfAccCategories.Where(x => x.ChartOfAccCategoryName == "FDR and DPS").Select(x => x.ChartOfAccCategoryId).FirstOrDefault();
                        chartofAcc = new Models.ChartofAcc
                        {
                            ChartOfAccName = details.BankShortCode + ": " + details.BankAccountNumber,
                            ChartOfAccCode = code.ToString(),
                            AccountId = details.AccountId,
                            ChartOfAccCategoryId = 1,
                            ChartOfAccCategoryName = "Current Assets",
                            IsActive = true,
                            ActionById = obj.ActionById,
                            BranchId = obj.BranchId,
                            LastActionDateTime = DateTime.Now,
                            TemplateId = 2
                        };

                        await _context.ChartofAccs.AddAsync(chartofAcc);
                        await _context.SaveChangesAsync();

                        details.ChartofAccId = chartofAcc.ChartofAccId;

                        _context.BankAccounts.Update(details);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        chartofAcc = new Models.ChartofAcc
                        {
                            ChartOfAccName = details.BankShortCode + ": " + details.BankAccountNumber,
                            ChartOfAccCode = code.ToString(),
                            AccountId = details.AccountId,
                            ChartOfAccCategoryId = 1,
                            ChartOfAccCategoryName = "Current Assets",
                            IsActive = true,
                            ActionById = obj.ActionById,
                            BranchId = obj.BranchId,
                            LastActionDateTime = DateTime.Now,
                            TemplateId = 2,

                        };

                        await _context.ChartofAccs.AddAsync(chartofAcc);
                        await _context.SaveChangesAsync();

                        details.ChartofAccId = chartofAcc.ChartofAccId;

                        _context.BankAccounts.Update(details);
                        await _context.SaveChangesAsync();
                    }

                }
                await transaction.CommitAsync();
                var msg = new MessageHelper { message = "Created Successfully", statusCode = 200 };
                return msg;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
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

        public async Task<List<BankAccountDTO>> GetBankAccountLandingPagination(long accountId, long branchId)
        {
            try
            {
                var data = (from h in _context.BankAccounts
                            where h.BranchId == branchId
                            && h.AccountId == accountId
                            select new BankAccountDTO
                            {
                                AccountId = h.AccountId,
                                BranchId = h.BranchId,
                                ActionById = h.ActionById,
                                BankAccHolderName = h.BankAccHolderName,
                                BankAccountId = h.BankAccountId,
                                BankAccountNumber = h.BankAccountNumber,
                                BankAccountTypeId = h.BankAccountTypeId,
                                BankAccountTypeName = h.BankAccountTypeName,
                                BankBranchId = h.BankBranchId,
                                BankBranchName = h.BankBranchName,
                                BankId = h.BankId,
                                BankName = h.BankName,
                                BankShortCode = h.BankShortCode,
                                ChartofAccId = h.ChartofAccId,
                                RoutingNumber = h.RoutingNumber
                            }).ToList();
                return data;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<object> GetBankAccountType()
        {
            try
            {
                var data = _context.BankAccountTypes.Select(x => new
                {
                    BankAccountTypeId = x.BankAccountTypeId,
                    BankAccountTypeNmae = x.BankAccountTypeName
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetBankBranch(long bankId)
        {
            try
            {
                var data = _context.BankBranches.Where(x => x.BankId == bankId && x.IsActive == true).Select(x => new
                {
                    BankBranchId = x.BankBranchId,
                    BankBranchCode = x.BankBranchCode,
                    BankBranchName = x.BankBranchName,
                    RoutingNumber = x.RoutingNo
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetBankName()
        {
            try
            {
                var data = _context.Banks.Where(x => x.IsActive == true).Select(x => new
                {
                    BankId = x.BankId,
                    BankName = x.BankName,
                    ShortName = x.ShortName
                }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            var data = _context.Partners.Where(x => x.AccountId == accountId && x.PartnerTypeId == partnerTypeId).Select(x => new PartnerDTO
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
