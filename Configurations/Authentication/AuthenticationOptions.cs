using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CrealutionServer.Configurations.Authentication
{
    public class AuthenticationOptions
    {
        public string Issuer { get; }
        public string Audience { get; }
        public int LifeTime { get; }

        private readonly string _secretKey;

        public AuthenticationOptions(
            string issuer, 
            string audience, 
            string secretKey, 
            int lifeTime)
        {
            Issuer = issuer;
            Audience = audience;
            LifeTime = lifeTime;
            _secretKey = secretKey;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
    }
}