using CRUD_JWT.Model;
using CRUD_JWT.DTO;


namespace CRUD_JWT.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterRequest Reg_Req);

        Task<LoginResponse> Login(LoginRequest Log_Req);
    }
}
