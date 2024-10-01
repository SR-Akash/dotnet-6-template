using MGM_Lite.DTO;
using MGM_Lite.DTO.ConfigurationDTO;
using MGM_Lite.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MGM_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _IRepository;

        public AccountsController(IAccountsService IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("CreateChartofAcc")]
        public async Task<IActionResult> CreateChartofAcc(ChartofAccDTO create)
        {
            return Ok((await _IRepository.CreateChartofAcc(create)));

        }

        [HttpPost]
        [Route("UpdateChartofAcc")]
        public async Task<IActionResult> UpdateChartofAcc(ChartofAccDTO create)
        {
            return Ok((await _IRepository.UpdateChartofAcc(create)));

        }

        [HttpPost]
        [Route("CreateChartofAccTemplate")]
        public async Task<IActionResult> CreateChartofAccTemplate(List<ChartofAccTemplateDTO> create)
        {
            return Ok((await _IRepository.CreateChartofAccTemplate(create)));

        }

        [HttpGet]
        [Route("GetChartofAccList")]
        public async Task<IActionResult> GetChartofAccList(long accountId, string? search)
        {
            return Ok((await _IRepository.GetChartofAccList(accountId, search)));

        }

        [HttpGet]
        [Route("GetChartofAccListTemplate")]
        public async Task<IActionResult> GetChartofAccListTemplate(long accountId, string? search)
        {
            return Ok((await _IRepository.GetChartofAccListTemplate(accountId, search)));

        }

        [HttpGet]
        [Route("GetChartOfAccCategoryList")]
        public async Task<IActionResult> GetChartOfAccCategoryList(long accountId)
        {
            return Ok((await _IRepository.GetChartOfAccCategoryList(accountId)));

        }

        [HttpGet]
        [Route("GetJournalVoucherLandingPagination")]
        public async Task<IActionResult> GetJournalVoucherLandingPagination(long accountId, DateTime? fromDate, DateTime? toDate, string? search, string viewOrder, long pageNo, long pageSize)
        {
            return Ok((await _IRepository.GetJournalVoucherLandingPagination(accountId, fromDate, toDate, search, viewOrder, pageNo, pageSize)));

        }

        [HttpGet]
        [Route("GetJournalVoucherById")]
        public async Task<IActionResult> GetJournalVoucherById(long journalId)
        {
            return Ok((await _IRepository.GetJournalVoucherById(journalId)));

        }

        [HttpGet]
        [Route("GetBankAccountList")]
        public async Task<IActionResult> GetBankAccountList(long accountId, long branchId)
        {
            return Ok((await _IRepository.GetBankAccountList(accountId,branchId)));

        }

        [HttpPost]
        [Route("CreateJournalVoucher")]
        public async Task<IActionResult> CreateJournalVoucher(JournalVoucherCommonDTO obj)
        {
            return Ok((await _IRepository.CreateJournalVoucher(obj)));

        }
    }
}
