using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Contracts.V1.Requests;
using Store.Helpers;
using Store.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services
{
    public class UserService : IUserService
    {
        private SignInManager<IdentityUser> signManager;
        private UserManager<IdentityUser> userManager;
        private readonly AppSettings appSettings;
        public async Task<string> GenerateToken(string email)
        {
            var user = await userManager.FindByNameAsync(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); ;

        }
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signManager = signInManager;
            this.appSettings = appSettings.Value;
        }

        public async Task<User> AddAsync(string email, string password)
        {
            
            var user = new IdentityUser { UserName = email };
            var result = await userManager.CreateAsync(user, password);
            var userResult = new User();
            if (result.Succeeded)
            { 
                userResult.Token = await GenerateToken(email);
            }
            else
            {
                userResult.Errors = result.Errors.Select(x => x.Description);
            }
            return userResult;
        }
        public async Task<User> LoginAsync(string email, string password)
        {
            var userResult = new User();
            var result = await signManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                userResult.Token = await  GenerateToken(email);
            }
            else {
                userResult.Errors = new[] {"Login Lub hasło nieprawidłowe"};
            }
            return userResult;
        }

     
    }
}
