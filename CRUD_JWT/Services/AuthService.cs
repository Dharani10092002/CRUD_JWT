using CRUD_JWT.Model;
using CRUD_JWT.DTO;
using Microsoft.EntityFrameworkCore;
using CRUD_JWT.Data;
using Scalar.AspNetCore;

namespace CRUD_JWT.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JWTServices _jwttoken;
        public AuthService(ApplicationDbContext _Context, JWTServices _jwt)
        {
            _dbContext = _Context;
            _jwttoken = _jwt;
        }

        public async Task<bool> Register(RegisterRequest Reg_Req)
        {
            var check_result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Reg_Req.Email);

            if (check_result != null)
            {
                return false;
            }

            var useradd = new User
            {
                Name = Reg_Req.Name,
                Email = Reg_Req.Email,
                PasswordHash= BCrypt.Net.BCrypt.HashPassword(Reg_Req.Password),
                Role = "User"

            };

            _dbContext.Users.Add(useradd);

           await _dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<LoginResponse?> Login(LoginRequest Log_Req)
        {
            var check_result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Log_Req.Email);

            if (check_result == null)
            {
                return null;
            }

            var passwordCheck = BCrypt.Net.BCrypt.Verify(Log_Req.Password, check_result.PasswordHash);

            if(!passwordCheck)
            {
                return null;
            }

            var tokenresult = _jwttoken.GenerateToken(check_result);

            return new LoginResponse
            {
                Token = tokenresult.token,
                Expiration = tokenresult.experitiondate
            };
        }
    }
}
