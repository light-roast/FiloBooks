using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ControlboxLibreriaAPI.Authentication
{
    public class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly FirebaseApp _firebaseApp;

        public FirebaseAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            FirebaseApp firebaseApp)
            : base(options, logger, encoder)
        {
            _firebaseApp = firebaseApp;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string? bearerToken = Context.Request.Headers["Authorization"];

            if(bearerToken == null || !bearerToken.StartsWith("Bearer " ))
            {
                return AuthenticateResult.Fail("Invalid scheme.");
            }

            string token = bearerToken.Substring("Bearer ".Length);

            try
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

                return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>()
                {
                new ClaimsIdentity(ToClaims(firebaseToken.Claims))
                }
                ), JwtBearerDefaults.AuthenticationScheme));
            }
            catch (Exception ex)
            {
                {
                    return AuthenticateResult.Fail(ex);
                }
            } 
            
        }

        private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, claims["user_id"].ToString()),
                new Claim(ClaimTypes.Email, claims["email"].ToString()),
                new Claim(ClaimTypes.Name, claims["name"].ToString())
            };
        }
    }
}
