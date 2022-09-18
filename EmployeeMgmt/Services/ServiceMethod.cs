using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EmployeeMgmt.Services
{
    public class ServiceMethod : IServiceMethod
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public ServiceMethod(UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Method added to generate Token
        /// </summary>      
        /// <returns></returns>
        public string GenerateToken(string requestUserName)
        {
            try
            {
                var claims =  GetClaims(requestUserName).Result;

                var maqtaAppSecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Audience:Secret"]));
                var token = new TokenBuilder()
                                              .AddSecurityKey(maqtaAppSecretKey)
                                              .AddSubject(requestUserName)
                                              .AddIssuer(_configuration["Audience:Iss"])
                                              .AddAudience(_configuration["Audience:Aud"])
                                              .AddClaims(claims)
                                              .Build();
                //// Below lines added to add user token information for further validation
                //var userTokenData = new AddUserTokenControllerRequest();
                //userTokenData.UserName = loginRequest.UserName;
                //userTokenData.TokenValue = token.Value;
                //userTokenData.CreatedBy = loginRequest.UserName;
                //AddUserTokenInfo(userTokenData).ConfigureAwait(false);

                return token.Value;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// GetCliams
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <param name="userName"></param>
        /// <param name="addExpiryMinutes"></param>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> GetClaims(string requestUserName)
        {
            try
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(requestUserName));

                Dictionary<string, string> claims = new Dictionary<string, string>();
                claims.Add("EMAIL", requestUserName);
                claims.Add(ClaimTypes.Role, string.Join(",", roles ?? new List<string>().ToArray()));
                return claims;
            }
            catch (Exception)
            {
                throw ;
            }
        }
    }
}
