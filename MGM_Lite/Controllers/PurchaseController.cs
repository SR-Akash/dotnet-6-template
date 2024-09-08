using MGM_Lite.DTO.ConfigurationDTO;
using MGM_Lite.DTO.PurchaseDTO;
using MGM_Lite.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MGM_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchase _IRepository;

        public PurchaseController(IPurchase IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpGet]
        [Route("GetPurchaseReceivedList")]
        public async Task<IActionResult> GetPurchaseReceivedList(long accountId)
        {
            return Ok((await _IRepository.GetPurchaseReceivedList(accountId)));

        }

        [HttpPost]
        [Route("CreatePurchaseReceive")]
        public async Task<IActionResult> CreatePurchaseReceive(PurchaseCommonDTO create)
        {
            return Ok((await _IRepository.CreatePurchaseReceive(create)));

        }

    }
}
