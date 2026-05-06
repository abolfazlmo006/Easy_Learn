using AutoMapper;
using Easy_Learn.UI.Contracts;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Easy_Learn.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IMessageSender _messageSender;
        public AuthenticationController(IMapper mapper, IClient client, IMessageSender messageSender)
        {
            _mapper = mapper;
            _client = client;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _messageSender = messageSender;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vM)
        {
            if (!ModelState.IsValid)
            {
                return View(vM);
            }
            var map = _mapper.Map<LoginDto>(vM);
            var apiresponse = await _client.LoginAsync(map);
            if (!apiresponse.SuccessFul)
            {
                if (apiresponse.Errors != null)
                {
                    ViewBag.Error = apiresponse.Errors;
                }
                else
                {
                    ViewBag.Message = apiresponse.Message;
                }
                return View(vM);
            }
            if (apiresponse.Data != string.Empty)
            {
                var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(apiresponse.Data);
                var claims = tokenContent.Claims.ToList();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, apiresponse.Data));
                claims.Add(new Claim(ClaimTypes.GivenName, apiresponse.FullName));
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, user);

            }
            return RedirectToAction("index", "home");

        }
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM vM)
        {
            var map = _mapper.Map<RegisterDto>(vM);
            var apiresponse = await _client.RegisterAsync(map);

            if (!apiresponse.SuccessFul)
            {
                if (apiresponse.Errors != null)
                {
                    ViewBag.Error = apiresponse.Errors;
                }
                else
                {
                    ViewBag.Message = apiresponse.Message;
                }
                return View(vM);
            }
            var url = Url.Action("ConfirmEmail", "Authentication", new { userName = vM.UserName, token = apiresponse.Data }, Request.Scheme);

            var htmlEnail = $"<a class='btn btn-info' href='{url}'>تایید ایمیل<a/>";

            await _messageSender.SendEmailAsync(vM.Email, "تایید ایمیل", htmlEnail,true);

            return View("ResponseRegister");
        }

        public async Task<ActionResult> ConfirmEmail(string Token, string userName)
        {
            var map = new ConfirmEmailDto()
            {
                Token = Token,
                UserName = userName
            };

            await _client.ConfirmEmailAsync(map);

            return RedirectToAction("Login", "Authentication");
        }
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Authentication");
        }
    }
}
