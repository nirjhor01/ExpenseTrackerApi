using ExpenseTrackerApi.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTrackerApi.JWT
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _iconfiguration;

        public JWTManagerRepository(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public Tokens GenerateToken(string userName)
        {
            return GenerateJWTTokens(userName);
        }

        public Tokens GenerateRefreshToken(string username)
        {
            return GenerateJWTTokens(username);
        }

        public Tokens GenerateJWTTokens(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var issuer = _iconfiguration["JWT:Issuer"];
                var audience = _iconfiguration["JWT:Audience"];

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                    }),

                    Expires = DateTime.Now.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return null;
            }
        }


        /*   public string GenerateRefreshToken()
           {
               var randomNumber = new byte[32];
               using (var rng = RandomNumberGenerator.Create())
               {
                   rng.GetBytes(randomNumber);
                   return Convert.ToBase64String(randomNumber);
               }
           }*/

        public string GenerateRefreshToken()
        {
            try
            {
                var expirationMinutes = int.Parse(_iconfiguration["JWT:RefreshTokenExpirationMinutes"]);
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    var refreshToken = Convert.ToBase64String(randomNumber);

                    // Add expiration time to the refresh token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var issuer = _iconfiguration["JWT:Issuer"];
                    var audience = _iconfiguration["JWT:Audience"];
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim("refreshToken", refreshToken)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"])), SecurityAlgorithms.HmacSha256Signature)
                    };

                    tokenHandler.CreateToken(tokenDescriptor);

                    return refreshToken;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return null;
            }
        }



        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
