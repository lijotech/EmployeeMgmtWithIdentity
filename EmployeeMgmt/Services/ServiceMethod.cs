using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeMgmt.Services
{
    public class ServiceMethod: IServiceMethod
    {
        private readonly IConfiguration _configuration;

        public ServiceMethod(
            IConfiguration configuration)
        {
            _configuration= configuration;
        }

        /// <summary>
        /// Method added to generate Token
        /// </summary>      
        /// <returns></returns>
        public string GenerateToken(string requestUserName)
        {
            try
            {

                var maqtaAppSecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Audience:Secret"]));
                var token = new TokenBuilder()
                                              .AddSecurityKey(maqtaAppSecretKey)
                                              .AddSubject(requestUserName)
                                              .AddIssuer("MR.Identity.API")
                                              .AddAudience("MR.Services")
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
    }
}
