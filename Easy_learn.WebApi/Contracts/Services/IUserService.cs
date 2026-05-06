global using Easy_learn.WebApi.Services.Base;
global using Easy_learn.WebApi.DTOs.UserDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IUserService
    {
        Task<Response> Register(RegisterDto dto);
        Task<LoginResponse> Login(LoginDto dto);
        Task<Response> ConfirmEmail(string userName ,string token );
        Task<Response> ForgotPassword(string Email);
        Task<Response> ResetPassword(ResetPasswordDto dto);
        Task<Response> ChangePassword(ChangePasswordDto dto , string userName);
        Task<ProfileUserDto> GetProfile(string userName);
    }
}
