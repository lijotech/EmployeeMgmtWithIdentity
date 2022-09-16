using System.IdentityModel.Tokens.Jwt;

namespace EmployeeMgmt.DTO
{
    public sealed class Token
    {
        private JwtSecurityToken token;

        internal Token(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }

}
