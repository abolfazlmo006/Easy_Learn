global using Easy_learn.WebApi.Contracts.Services;
global using Microsoft.AspNetCore.Identity;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Easy_learn.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMessageSender _messageSender;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(UserManager<UserEntity> userManager, IMapper mapper, IConfiguration configuration, IMessageSender messageSender, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _messageSender = messageSender;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> ChangePassword(ChangePasswordDto dto , string userName)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ChangePasswordAsync(user, dto.currentPassword, dto.newPassword);
            if (!result.Succeeded)
            {
                response.errors = result.Errors.Select(e => e.Description).ToList();
                response.Message = "عملیات تغییر رمز عبور با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            response.Message = "عملیات تغییر رمز عبور با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task<Response> ConfirmEmail(string userName, string token)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.Message = "کاربری با این مشخصات یافت نشد";
                response.SuccessFul = false;
                return response;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                response.Message = "تایید ایمیل با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            response.Message = "تایید ایمیل با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task<Response> ForgotPassword(string Email)
        {
            var response = new Response();
            response.SuccessFul = true;
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                response.SuccessFul = false;
            }else
            {
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                response.Data = resetPasswordToken;

                // Test Email Send
                await _messageSender.SendEmailAsync(user.Email, "فراموشی کلمه عبور", resetPasswordToken);
            }

            response.Message = "اگر ایمیل وارد شده درست باشد لینک فراموشی کلمه عبور برای شما فرستاده خواهد شد";
            return response;
        }


        // is not optimized
        public async Task<ProfileUserDto> GetProfile(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var comment = await _unitOfWork.Comment.GetCommentByUser(userName);
            var course = await _unitOfWork.Course.GetCourseByUser(user.Id);
            var question = await _unitOfWork.QuestionCourse.GetQuestionCourseListForUser(user.Id);
            var answer = await _unitOfWork.AnswerQuestion.GetForUser(user.Id);
            var profile = new ProfileUserDto()
            {
                Email = user.Email,
                answers = answer.TakeLast(3).ToList(),
                AnswerCount = answer.Count(),
                comments = comment.TakeLast(3).ToList(),
                CommentCount = comment.Count(),
                courses = course.TakeLast(3).ToList(),
                CourseCount = course.Count(),
                QuestionCount = question.Count(),
                questions = question.TakeLast(3).ToList()
            };
            return profile;
        }

        public async Task<LoginResponse> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var response = new LoginResponse();
                response.Message = "اطلاعات اشتباه است";
                response.SuccessFul = false;
                return response;
            }

            if (!user.EmailConfirmed)
            {
                var response = new LoginResponse();
                response.Message = "لطفاً ایمیل خود را تایید کنید";
                response.SuccessFul = false;
                return response;
            }

            var roleClaim = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaim.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(roleClaim);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResponse
            {
                Message = "ورود با موفقیت انجام شد",
                SuccessFul = true,
                Data = new JwtSecurityTokenHandler().WriteToken(token),
                FullName = user.Full_Name
            };
        }

        public async Task<Response> Register(RegisterDto dto)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user != null)
            {
                response.Message = "کاربری با این مشخصات قبلاً ثبت شده است";
                response.SuccessFul = false;
                return response;
            }
            var userEntity = _mapper.Map<UserEntity>(dto);
            var result = await _userManager.CreateAsync(userEntity, dto.Password);
            if (!result.Succeeded)
            {
                response.errors = result.Errors.Select(e => e.Description).ToList();
                response.Message = "ثبت نام با خطا مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            var emailConfirmationToken =
                        await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);


            // Test Email Send
            // await _messageSender.SendEmailAsync(dto.Email, "تایید ایمیل", emailConfirmationToken);

            await _userManager.AddToRoleAsync(userEntity, "User");
            response.Data = emailConfirmationToken;
            response.Message = "کاربر با موفقیت ثبت شد و لینک تایید ایمیل برای شما ارسال شد";
            response.SuccessFul = true;
            return response;

        }

        public async Task<Response> ResetPassword(ResetPasswordDto dto)
        {
            var response = new Response();
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
            if (!result.Succeeded)
            {
                response.errors = result.Errors.Select(result => result.Description).ToList();
                response.Message = "عملیات تغییر رمز عبور با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            response.Message = "عملیات تغییر رمز عبور با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

    }
}
