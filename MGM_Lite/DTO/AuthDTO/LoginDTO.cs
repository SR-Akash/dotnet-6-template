namespace MGM_Lite.DTO.AuthDTO
{
    public class LoginDTO
    {

    }

    public class AuthDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public int expires_in { get; set; }
        public string ActionTime { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }

    public class SmeUserInfoDTO
    {
        public long AccountId { get; set; }
        public long UserId { get; set; }
        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }    
        public string UserName { get; set; }
        public string MobileNumber { get; set; }     
        public string Email { get; set; }
        public bool? isMasterUser { get; set; }
        public AuthDTO auth { get; set; }
        
    }

    public class UserLogInDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
