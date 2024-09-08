using MGM_Lite.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System;
using MGM_Lite.DTO.AuthDTO;
using MGM_Lite.DBContext;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace MGM_Lite.Repository.Auth
{
    public class Login : ILogin
    {
        public readonly AppDbContext _context;
        private readonly Audience _audience;
        private IConfiguration _configuration;
        public Login(AppDbContext context, IOptions<Audience> audienceOptions, IConfiguration configuration)
        {
            _context = context;
            _audience = audienceOptions.Value;
            _configuration = configuration;
        }

        public async Task<SmeUserInfoDTO> UserLogIn(string userid, string password)
        {
            try
            {
                var isBlock = _context.Users.Where(x => (x.Mobile == userid || x.Email == userid || x.Email == userid) && x.IsActive == true).Select(x => x).FirstOrDefault();


                var data = await Task.FromResult((from e in _context.Users
                                                  where (e.Mobile.Trim() == userid.Trim() || e.Email.Trim() == userid.Trim())
                                                  && e.Password.Trim() == password.Trim() && e.IsActive == true
                                                  select e).FirstOrDefault());

                if (data == null)
                {
                    throw new Exception("Login Failed! Check your Login Credentials");
                }

                var employeeDetails = await Task.FromResult((from e in _context.Employees
                                                             where e.EmployeeId == data.EmployeeId
                                                             select e).FirstOrDefault());

                var userData = await Task.FromResult((from e in _context.Users
                                                      where e.UserId == data.UserId
                                                      select e).FirstOrDefault());

                AuthDTO token;

                token = await GenerateToken(userid);


                SmeUserInfoDTO usrInfo = new SmeUserInfoDTO();

                usrInfo.UserId = data.UserId;
                usrInfo.AccountId = data.AccountId;
                usrInfo.UserName = data.UserName;
                usrInfo.Email = data.Email;
                usrInfo.MobileNumber = data.Mobile;
                usrInfo.isMasterUser = data.IsMasterUser;
                usrInfo.auth = token;
                usrInfo.EmployeeId = employeeDetails != null ? employeeDetails.EmployeeId : 0;
                usrInfo.EmployeeName = employeeDetails != null ? employeeDetails.EmployeeName : "";

                return await Task.FromResult(usrInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<AuthDTO> GenerateToken(string userId)
        {
            var now = DateTime.UtcNow;
            try
            {
                var claims = new Claim[]
               {
                    new Claim("userId",userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, "test"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                }; 

                var audienceConfig = _configuration.GetSection("Audience");
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

                var jwt = new JwtSecurityToken(
                    issuer: _audience.Iss,
                    audience: _audience.Aud,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromDays(60 * 24 * 1)), 
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                return new AuthDTO
                {
                    Success = true,
                    Token = encodedJwt,
                    RefreshToken = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(1).TotalSeconds,
                    ActionTime =DateTime.Now.ToString()
                };

            }
            catch (Exception ex)
            {
                throw new Exception("data not match");
            }
        }
    }

}
