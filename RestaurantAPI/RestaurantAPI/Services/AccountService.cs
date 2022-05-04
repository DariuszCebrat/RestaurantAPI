using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
    }
    public class AccountService:IAccountService
    {
        private readonly RestaurantDbContext _db;
        private readonly IPasswordHasher<User> _passwordHaser;

        public AccountService(RestaurantDbContext db,IPasswordHasher<User> passwordHaser)
        {
            _db = db;
            _passwordHaser = passwordHaser;
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
