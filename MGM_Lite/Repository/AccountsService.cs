using MGM_Lite.DBContext;
using MGM_Lite.DTO;
using MGM_Lite.IRepository;

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
                    var updateList = new List<Models.ChartofAcc>();
                    foreach(var itm in existingData)
                    {
                        itm.IsActive = false;
                        updateList.Add(itm);
                    }
                    _context.ChartofAccs.UpdateRange(updateList);
                    await _context.SaveChangesAsync();
                }

                var newList = create.Select(x => new Models.ChartofAcc
                {
                    AccountId = accountId,
                    ActionById = x.ActionById,
                    BranchId = x.BranchId,
                    ChartOfAccCategoryId = x.ChartOfAccCategoryId,
                    ChartOfAccCategoryName = x.ChartOfAccCategoryName,
                    ChartOfAccCode = x.ChartOfAccCode,
                    ChartOfAccName = x.ChartOfAccName,
                    IsActive = true,
                    LastActionDateTime = DateTime.Now,
                    TemplateId = x.TemplateId
                }).ToList();

                await _context.ChartofAccs.AddRangeAsync(newList);
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
                                ChartOfAccName = c.ChartOfAccName
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
    }
}

