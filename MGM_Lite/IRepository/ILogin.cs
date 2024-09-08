using MGM_Lite.DTO.AuthDTO;

namespace MGM_Lite.IRepository
{
    public interface ILogin
    {
        Task<SmeUserInfoDTO> UserLogIn(string userid, string password);
    }
}
