using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Azure.Core;

namespace AssetManager.Classes
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var userInfo = await SecureStorage.GetAsync("accounttoken");
                if(userInfo != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "testuser") };
                    identity = new ClaimsIdentity(claims, "Server Authentication");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Request failed." + e.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Login(string token)
        {
            await SecureStorage.SetAsync("accounttoken", token);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            SecureStorage.Remove("accounttoken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
