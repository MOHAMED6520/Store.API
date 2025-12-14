using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.API.Domain.Exceptions.ValidationException;
using Store.API.Domain.Models.Identity;
using Store.API.Services.Abstractions.AuthServices;
using Store.API.Shared;
using Store.API.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ValidationException = Store.API.Domain.Exceptions.ValidationException.ValidationException;

namespace Store.API.Services.AuthServices
{
    public class AuthService(UserManager<AppUser> _userManager , IOptions<JwtOptions> Options) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnauthorizedAccessException();
            var flag = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!flag) throw new UnauthorizedAccessException();

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTooken(user)
            };
            
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            
            var EmailCheek =await _userManager.FindByEmailAsync(registerDto.Email);
            if (EmailCheek is not null)
            {
                throw new ValidationException("This Email is already exist");
            }
            var User = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
           var result = await _userManager.CreateAsync(User, registerDto.Password);
          
            if (!result.Succeeded)
            {
               
                IEnumerable<string> errors = result.Errors.Select(E => E.Description);
               
                throw new ValidationException(errors);
            }
            return new UserResultDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await GenerateJwtTooken(User)
            };
        }



        private async Task<string> GenerateJwtTooken(AppUser user)
        {
            var option = Options.Value;

            var authclaims = new List<Claim>()
            {
                new Claim (ClaimTypes.Name,user.UserName),
                new Claim (ClaimTypes.Email,user.Email)
            };
            var roles =await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.SecretKey));

            var Tooken = new JwtSecurityToken
                (
                issuer: option.Issuer,
                audience : option.Audience,
                claims: authclaims,
                expires:DateTime.UtcNow.AddDays(option.DurationOnDays),
                signingCredentials:new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(Tooken);

        }
    }
}
