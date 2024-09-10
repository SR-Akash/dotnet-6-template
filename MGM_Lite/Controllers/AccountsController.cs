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
            return Ok((await _IRepository.GetChartofAccList(accountId,search)));

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
    }
}
