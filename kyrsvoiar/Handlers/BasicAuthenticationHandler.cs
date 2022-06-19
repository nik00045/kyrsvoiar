using kyrsvoiar.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace kyrsvoiar.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly kyrsarbdContext _context;

        public BasicAuthenticationHandler(
           IOptionsMonitor<AuthenticationSchemeOptions> option,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
           kyrsarbdContext context) : base(option, logger , encoder , clock)
        {
            _context = context;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("need header");

            try {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credent = Encoding.UTF8.GetString(bytes).Split(":");
                string email = credent[0];
                string pass = credent[1];

                Owner user = _context.Owner.Where(user => user.Mail == email && user.Password == pass).FirstOrDefault();

                if (user == null)
                {
                     AuthenticateResult.Fail("fail data");
                }
                else {
                    var claim = new[] { new Claim(ClaimTypes.Name, user.Mail) };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var tiket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(tiket);
                }

            } catch (Exception) {

                return AuthenticateResult.Fail("error");
            }
           return AuthenticateResult.Fail("");
        }
    }
}
