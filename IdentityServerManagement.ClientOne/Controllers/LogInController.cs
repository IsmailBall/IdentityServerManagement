using IdentityModel.Client;
using IdentityServerManagement.ClientOne.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;

namespace IdentityServerManagement.ClientOne.Controllers
{
    public class LogInController : Controller
    {

        private readonly IConfiguration _configuration;

        public LogInController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);

            if (disco.IsError)
            {
                //hata yakalama ve loglama
            }

            var password = new PasswordTokenRequest();

            password.Address = disco.TokenEndpoint;
            password.UserName = logInDto.Email;
            password.Password = logInDto.Password;
            password.ClientId = _configuration["ClientResourceOwner:client_id"];
            password.ClientSecret = _configuration["ClientResourceOwner:client_secret"];

            var token = await client.RequestPasswordTokenAsync(password);

            if (token.IsError)
            {
                ModelState.AddModelError("", "Email veya şifreniz yanlış");
                return View();

                //hata yakalama ve loglama
            }

            var userinfoRequest = new UserInfoRequest();

            userinfoRequest.Token = token.AccessToken;
            userinfoRequest.Address = disco.UserInfoEndpoint;
            var userinfo = await client.GetUserInfoAsync(userinfoRequest);

            if (userinfo.IsError)
            {
                //hata yakalama ve loglama
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userinfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                      new AuthenticationToken{ Name=OpenIdConnectParameterNames.AccessToken,Value= token.AccessToken},
                            new AuthenticationToken{ Name=OpenIdConnectParameterNames.RefreshToken,Value= token.RefreshToken},
                                  new AuthenticationToken{ Name=OpenIdConnectParameterNames.ExpiresIn,Value= DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)}
            });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return RedirectToAction("Index", "Users");
        }
    }
}
