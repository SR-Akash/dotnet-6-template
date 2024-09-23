using MGM_Lite.DBContext;
using MGM_Lite.DTO;
using MGM_Lite.IRepository;
using MGM_Lite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MGM_Lite.Repository
{
    public class AccountsService : IAccountsService
    {
        private readonly AppDbContext _context;
        public AccountsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateChartofAcc(ChartofAccDTO create)
        {
            try
            {
                var check = _context.ChartofAccs.Where(x => x.AccountId == create.AccountId && x.ChartOfAccName.Trim().ToLower() == create.ChartOfAccName.Trim().ToLower()).FirstOrDefault();
                if (check != null)
                {
                    throw new Exception("Chart of acc name already exist.");
                }

                var data = new Models.ChartofAcc
                {
                    AccountId = create.AccountId,
                    ActionById = create.ActionById,
                    BranchId = create.BranchId,
                    ChartOfAccCategoryId = create.ChartOfAccCategoryId,
                    ChartOfAccCategoryName = create.ChartOfAccCategoryName,
                    ChartOfAccCode = create.ChartOfAccCode,
                    ChartOfAccName = create.ChartOfAccName,
                    IsActive = true,
                    LastActionDateTime = DateTime.Now,
                    TemplateId = 0
                };
                await _context.ChartofAccs.AddAsync(data);
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

        public async Task<MessageHelper> CreateChartofAccTemplate(List<ChartofAccTemplateDTO> create)
        {
            try
            {
                long accountId = create.Select(x => x.AccountId).FirstOrDefault();

                var existingData = _context.ChartofAccs.Where(x => x.AccountId == accountId && x.TemplateId > 0).ToList();
                if (existingData.Any())
                {
                    var inActiveList = new List<Models.ChartofAcc>();
                    foreach (var itm in existingData)
                    {
                        itm.IsActive = false;
                        inActiveList.Add(itm);
                    }
                    _context.ChartofAccs.UpdateRange(inActiveList);
                    await _context.SaveChangesAsync();
                }

                var updateList = new List<Models.ChartofAcc>(create.Count());
                var newList = new List<Models.ChartofAcc>(create.Count());
                foreach (var itm in create)
                {
                    var coa = _context.ChartofAccs.Where(x => x.AccountId == accountId && x.ChartOfAccName.Trim().ToLower() == itm.ChartOfAccName.Trim().ToLower()).Select(x => x).FirstOrDefault();
                    if (coa != null)
                    {
                        coa.IsActive = true;
                        updateList.Add(coa);
                    }
                    else
                    {
                        var coaData = new Models.ChartofAcc
                        {
                            AccountId = accountId,
                            ActionById = itm.ActionById,
                            BranchId = itm.BranchId,
                            ChartOfAccCategoryId = itm.ChartOfAccCategoryId,
                            ChartOfAccCategoryName = itm.ChartOfAccCategoryName,
                            ChartOfAccCode = itm.ChartOfAccCode,
                            ChartOfAccName = itm.ChartOfAccName,
                            IsActive = true,
                            LastActionDateTime = DateTime.Now,
                            TemplateId = itm.TemplateId
                        };
                        newList.Add(coaData);
                    }
                }

                if (updateList.Any())
                {
                    _context.ChartofAccs.UpdateRange(updateList);
                    await _context.SaveChangesAsync();
                }

                if (newList.Any())
                {
                    await _context.ChartofAccs.AddRangeAsync(newList);
                    await _context.SaveChangesAsync();
                }



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

        public async Task<List<ChartOfAccCategoryDTO>> GetChartOfAccCategoryList(long accountId)
        {
            try
            {
                var data = (from r in _context.ChartOfAccCategories
                            where r.AccountId == 0 && r.IsActive == true
                            select new ChartOfAccCategoryDTO
                            {
                                AccountId = r.AccountId,
                                ChartOfAccCategoryId = r.ChartOfAccCategoryId,
                                ChartOfAccCategoryName = r.ChartOfAccCategoryName
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChartofAccDTO>> GetChartofAccList(long accountId, string? search)
        {
            try
            {
                var data = (from c in _context.ChartofAccs
                            where (c.AccountId == 0 || c.AccountId == accountId)
                            && (search == null || (c.ChartOfAccName.Contains(search))
                            || (c.ChartOfAccCode.Contains(search))
                            || (c.ChartOfAccCategoryName.Contains(search)))
                            && c.IsActive == true
                            orderby c.ChartOfAccCategoryName, c.ChartOfAccName
                            select new ChartofAccDTO
                            {
                                IsActive = c.IsActive,
                                AccountId = c.AccountId,
                                ActionById = c.ActionById,
                                ActionByName = "",
                                BranchId = c.BranchId,
                                ChartOfAccCategoryId = c.ChartOfAccCategoryId,
                                ChartOfAccCategoryName = c.ChartOfAccCategoryName,
                                ChartOfAccCode = c.ChartOfAccCode,
                                ChartofAccId = c.ChartofAccId,
                                ChartOfAccName = c.ChartOfAccName,
                                TemplateId = c.TemplateId
                            }).ToList();

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ChartofAccTemplateDTO>> GetChartofAccListTemplate(long accountId, string? search)
        {
            try
            {
                var data = (from c in _context.ChartofAccTemplates
                            where (c.AccountId == 0)
                            && (search == null || (c.ChartOfAccName.Contains(search))
                            || (c.ChartOfAccCode.Contains(search))
                            || (c.ChartOfAccCategoryName.Contains(search)))
                            && c.IsActive == true
                            let isChecked = _context.ChartofAccs.Where(x => x.TemplateId == c.TemplateId && x.IsActive).Select(x => x).FirstOrDefault()
                            select new ChartofAccTemplateDTO
                            {
                                AccountId = c.AccountId,
                                ChartOfAccCategoryId = c.ChartOfAccCategoryId,
                                ChartOfAccCategoryName = c.ChartOfAccCategoryName,
                                ChartOfAccCode = c.ChartOfAccCode,
                                TemplateId = c.TemplateId,
                                ChartOfAccName = c.ChartOfAccName,
                                IsPreviouslyChecked = isChecked != null ? true : false
                            }).ToList();

                return data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<MessageHelper> UpdateChartofAcc(ChartofAccDTO create)
        {
            try
            {
                var data = _context.ChartofAccs.Where(x => x.ChartofAccId == create.ChartofAccId).Select(x => x).FirstOrDefault();
                if (data != null)
                {
                    data.ChartOfAccName = create.ChartOfAccName;
                    data.ChartOfAccCode = create.ChartOfAccCode;
                    data.ChartOfAccCategoryId = create.ChartOfAccCategoryId;
                    data.ChartOfAccCategoryName = create.ChartOfAccCategoryName;
                    _context.ChartofAccs.Update(data);
                    await _context.SaveChangesAsync();
                }

                return new MessageHelper
                {
                    message = "Update successfully",
                    statusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JournalVoucherLandingPaginationDTO> GetJournalVoucherLandingPagination(long accountId, DateTime? fromDate, DateTime? toDate, string? search, string viewOrder, long pageNo, long pageSize)
        {
            var totalCount = _context.SubLedgerHeaders.Where(x => x.AccountId == accountId && x.IsActive == true).Select(x => x.SubLedgerHeaderId).Count();
            var itemdata = (from x in _context.SubLedgerHeaders

                            where x.AccountId == accountId
                            && (fromDate == null || x.TransactionDate.Date >= fromDate.Value.Date)
                            && (toDate == null || x.TransactionDate.Date <= toDate.Value.Date)
                            && (search == null
                            || x.SubLedgerCode.Trim().Contains(search.Trim())
                            || x.TransactionTypeName.Trim().Contains(search.Trim())
                            || x.TransactionCode.Trim().Contains(search.Trim()))
                             && x.IsActive == true
                            orderby x.TransactionDate.Date descending, x.SubLedgerHeaderId descending

                            select new JournalVoucherHeaderDTO
                            {
                                SubLedgerHeaderId = x.SubLedgerHeaderId,
                                SubLedgerCode = x.SubLedgerCode,
                                AccountId = x.AccountId,
                                BranchId = x.BranchId,
                                Narration = x.Narration,
                                Amount = _context.SubLedgerRows.Where(a => a.SubLedgerHeaderId == x.SubLedgerHeaderId && a.Amount > 0).Select(b => b.Amount).Sum(),
                                TransactionId = x.TransactionId,
                                TransactionCode = x.TransactionCode,
                                TransactionTypeId = x.TransactionTypeId,
                                TransactionTypeName = x.TransactionTypeName,
                                InstrumentType = x.InstrumentType,
                                ActionById = x.ActionById,
                                ActionByName = x.ActionByName,
                                TransactionDate = x.TransactionDate
                            }).Skip((int)((pageNo - 1) * pageSize)).Take((int)pageSize).ToList();


            if (pageNo <= 0)
                pageNo = 1;

            var finalData = itemdata.Where(x => x.Amount > 0).ToList();
            long index = 1 + ((pageNo - 1) * pageSize);
            foreach (var item in finalData)
            {
                item.Sl = index;
                index++;
            }

            var itm = new JournalVoucherLandingPaginationDTO
            {
                data = finalData,
                CurrentPage = pageNo,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            return itm;
        }

        public async Task<JournalVoucherCommonDTO> GetJournalVoucherById(long journalId)
        {
            try
            {
                var headerData = (from x in _context.SubLedgerHeaders

                                  where x.SubLedgerHeaderId == journalId
                                  orderby x.TransactionDate.Date descending, x.SubLedgerHeaderId descending

                                  select new JournalVoucherHeaderDTO
                                  {
                                      SubLedgerHeaderId = x.SubLedgerHeaderId,
                                      SubLedgerCode = x.SubLedgerCode,
                                      AccountId = x.AccountId,
                                      BranchId = x.BranchId,
                                      Narration = x.Narration,
                                      Amount = _context.SubLedgerRows.Where(a => a.SubLedgerHeaderId == x.SubLedgerHeaderId && a.Amount > 0).Select(b => b.Amount).Sum(),
                                      TransactionId = x.TransactionId,
                                      TransactionCode = x.TransactionCode,
                                      TransactionTypeId = x.TransactionTypeId,
                                      TransactionTypeName = x.TransactionTypeName,
                                      InstrumentType = x.InstrumentType,
                                      ActionById = x.ActionById,
                                      ActionByName = x.ActionByName,
                                      TransactionDate = x.TransactionDate,
                                      InstrumentNo = x.InstrumentNo
                                  }).FirstOrDefault();

                var rowsData = (from a in _context.SubLedgerRows
                                join c in _context.ChartofAccs on a.ChartOfAccId equals c.ChartofAccId

                                where a.SubLedgerHeaderId == journalId
                                select new JournalVoucherRowDTO
                                {
                                    ChartOfAccId = a.ChartOfAccId,
                                    ChartOfAccName = c.ChartOfAccName,
                                    Amount = a.Amount,
                                    PartnerId = 0,
                                    PartnerName = a.PartnerId > 0 ? _context.Partners.Where(x => x.PartnerId == a.PartnerId
                                                      ).Select(x => x.PartnerName).FirstOrDefault() : "",

                                    ChartOfAccCode = c.ChartOfAccCode,
                                    SubLedgerHeaderId = a.SubLedgerHeaderId,
                                    RowId = a.RowId
                                }).ToList();

                return new JournalVoucherCommonDTO
                {
                    header = headerData,
                    rows = rowsData
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

