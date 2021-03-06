using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Tokens;
using projekt.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace projekt.Controllers
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly LibraryDbContext _context;
        private readonly IPasswordHasher<User> _hasher;
        private readonly AuthenticationSettings _settings;
        public AccountService(LibraryDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _hasher = passwordHasher;
            _settings = authenticationSettings;
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Name} {user.LastName}".ToString()),
                new Claim(ClaimTypes.Role,$"{user.Role.Name}"),
                


            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_settings.JwtExpireDays);
            var token = new JwtSecurityToken(_settings.JwtIssuer, _settings.JwtIssuer,claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var user = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                LastName = dto.LastName,
                PasswordHash = dto.Password,
                RoleId = dto.RoleId,

            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

        }

    }
}
