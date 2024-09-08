using MGM_Lite.DTO.AuthDTO;
using MGM_Lite.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MGM_Lite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogin _IRepository;

        public AuthController(ILogin IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("UserLogIn")]
        public async Task<IActionResult> UserLogIn([FromBody] UserLogInDTO user)
        {
            return Ok((await _IRepository.UserLogIn(user.UserName, user.Password)));

        }
    }
}
