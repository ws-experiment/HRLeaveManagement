using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        //object to access the context of the http requests
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, ILocalStorageService localStorage,  IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
            :base(client, localStorage)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                //new C# object declaration 
                AuthRequest authRequest = new() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    //get claims from token and build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                   
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue("token", authenticationResponse.Token);

                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                await Authenticate(registration.Email, registration.Password);
                return true;
            }
            return false;
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
