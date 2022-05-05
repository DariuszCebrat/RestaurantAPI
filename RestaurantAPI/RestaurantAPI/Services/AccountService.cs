using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }
    public class AccountService:IAccountService
    {
        private readonly RestaurantDbContext _db;
        private readonly IPasswordHasher<User> _passwordHaser;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(RestaurantDbContext db,IPasswordHasher<User> passwordHaser,AuthenticationSettings authenticationSettings)
        {
            _db = db;
            _passwordHaser = passwordHaser;
           _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _db.Users.Include(x=>x.Role).FirstOrDefault(x=>x.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
           var result =  _passwordHaser.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LasttName}"),
                new Claim(ClaimTypes.Role , $"{user.Role.Name}"),
                new Claim("DateOfBirth",user.DateOfBirth.Value.ToString()),
                new Claim("Nationality",user.Nationality)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,claims,expires:expires,signingCredentials:cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async  Task RegisterUserAsync(RegisterUserDto dto)
        {
            var newUser = new User
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId

            };
            var hashedPassword = _passwordHaser.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
          await   _db.Users.AddAsync(newUser);
           await  _db.SaveChangesAsync();
        }
    }
}
