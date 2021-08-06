using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecureAPI.Services
{
    public class AuthService
    {
        IConfiguration configuration;
        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;
        //1.
        public AuthService(IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        //2.
        public async Task<bool> RegisterUserAsync(RegisterUser register)
        {
            bool IsCreated = false;
            var registerUser = new IdentityUser() { UserName = register.Email, Email = register.Email };
            var result = await userManager.CreateAsync(registerUser, register.Password);
            if (result.Succeeded)
            {
                IsCreated = true;
            }
            return IsCreated;
        }
        
        //3
        public async Task<string> AuthenticateUserAsync(LoginUser inputModel)
        {

            string jwtToken = "";
 
            var result = await signInManager.PasswordSignInAsync(inputModel.UserName, inputModel.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                
                var secretKey = Convert.FromBase64String(configuration["JWTSettings:SecretKey"]);
                var expiryTimeSpan = Convert.ToInt32(configuration["JWTSettings:ExpiryInMinuts"]);
               

                IdentityUser user = new IdentityUser(inputModel.UserName);

                var securityTokenDescription = new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("username",user.Id,ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryTimeSpan),
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                };
                
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwToken = jwtHandler.CreateJwtSecurityToken(securityTokenDescription);
                jwtToken = jwtHandler.WriteToken(jwToken);
            }
            else
            {
                jwtToken = "Login failed";
            }

            return jwtToken;
        }
    }
}
