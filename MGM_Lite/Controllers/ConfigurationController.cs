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
    }
}
