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
                if (userInfo != null)
                {
                    ConnectionDB connection = new ConnectionDB();
                    connection.OpenConnection();
                    string query = "SELECT role FROM dbo.[user] WHERE email='" + userInfo + "'";
                    Microsoft.Data.SqlClient.SqlDataReader dr = connection.DataReader(query);
                    int role = 0;
                    while (dr.Read())
                    {
                        role = dr.GetInt32(0);
                    }
                    string roleString;
                    if (role == 0)
                        roleString = "Admin";
                    else
                        roleString = "User";
                    connection.CloseConnection();
					
					var claims = new[] { new Claim(ClaimTypes.Name, userInfo), new Claim(ClaimTypes.Role, roleString) };
                    identity = new ClaimsIdentity(claims, "Server Authentication");
                    
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
                else
                {
                    return new AuthenticationState(new ClaimsPrincipal());
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Request failed." + e.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal());
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
