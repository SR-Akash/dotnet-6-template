using MGM_Lite.DTO.AuthDTO;
using MGM_Lite.DTO.ConfigurationDTO;
using MGM_Lite.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MGM_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationModule _IRepository;

        public ConfigurationController(IConfigurationModule IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpGet]
        [Route("GetItemList")]
        public async Task<IActionResult> GetItemList(long accountId)
        {
            return Ok((await _IRepository.GetItemList(accountId)));

        }

        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(ItemsDTO create)
        {
            return Ok((await _IRepository.CreateItem(create)));

        }

        [HttpGet]
        [Route("GetPartnerList")]
        public async Task<IActionResult> GetPartnerList(long accountId, long partnerTypeId)
        {
            return Ok((await _IRepository.GetPartnerList(accountId, partnerTypeId)));

        }

        [HttpPost]
        [Route("CreatePartner")]
        public async Task<IActionResult> CreatePartner(PartnerDTO create)
        {
            return Ok((await _IRepository.CreatePartner(create)));

        }


        [HttpGet]
        [Route("GetBankName")]
        public async Task<IActionResult> GetBankName()
        {
            return Ok((await _IRepository.GetBankName()));

        }


        [HttpGet]
        [Route("GetBankBranch")]
        public async Task<IActionResult> GetBankBranch(long bankId)
        {
            return Ok((await _IRepository.GetBankBranch(bankId)));

        }


        [HttpGet]
        [Route("GetBankAccountType")]
        public async Task<IActionResult> GetBankAccountType()
        {
            return Ok((await _IRepository.GetBankAccountType()));

        }

        [HttpPost]
        [Route("CreateBankAccount")]
        public async Task<IActionResult> CreateBankAccount(BankAccountDTO obj)
        {
            return Ok((await _IRepository.CreateBankAccount(obj)));

        }

        [HttpGet]
        [Route("GetBankAccountLandingPagination")]
        public async Task<IActionResult> GetBankAccountLandingPagination(long accountId, long branchId)
        {
            return Ok((await _IRepository.GetBankAccountLandingPagination(accountId,branchId)));

        }
    }
}
