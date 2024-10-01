using MGM_Lite.DTO;
#pragma warning disable

namespace MGM_Lite.IRepository
{
    public interface IAccountsService
    {
        Task<List<ChartofAccDTO>> GetChartofAccList(long accountId, string? search);
        Task<List<ChartofAccTemplateDTO>> GetChartofAccListTemplate(long accountId, string? search);
        Task<MessageHelper> CreateChartofAcc(ChartofAccDTO create);
        Task<MessageHelper> UpdateChartofAcc(ChartofAccDTO create);
        Task<MessageHelper> CreateChartofAccTemplate(List<ChartofAccTemplateDTO> create);
        Task<List<ChartOfAccCategoryDTO>> GetChartOfAccCategoryList(long accountId);
        Task<JournalVoucherLandingPaginationDTO> GetJournalVoucherLandingPagination(long accountId, DateTime? fromDate, DateTime? toDate, string? search, string viewOrder, long pageNo, long pageSize);
        Task<JournalVoucherCommonDTO> GetJournalVoucherById(long journalId);
        Task<object> GetBankAccountList(long accountId, long branchId);
        Task<MessageHelper> CreateJournalVoucher(JournalVoucherCommonDTO obj);
    }
}
