using Interact.GateInvitations.AdminPanel.ViewModels;
using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Common.Enums;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Interact.GateInvitations.AdminPanel.Controllers
{
    public class AuthController : MainController
    {
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;

        public AuthController(IUserService userService, IAdminService adminService = null)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SignIn(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            ViewData["ReturnUrl"] = model.ReturnUrl;
            var validateCredintials =await _userService.CheckForUserCredintialsIsValidAsync(model.Username, model.Password,UserType.Admin);
            if (!validateCredintials.isValid)
            {
                ViewData["errorSignin"] = "Username or password is invalid";
                return View();
            }
            var admin =await _adminService.GetAdminByIdAsync(validateCredintials.validatedUser.Id);
            await SignAdminInAsync(model,admin);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult LogOut()
        {
            LogoutAdmin().Wait();
            return RedirectPermanent("/Auth/SignIn");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Utilities
        private async Task LogoutAdmin()
        {

            await HttpContext.SignOutAsync(AdminConfig.AdminAuthenticationScheme);
            HttpContext.Response.Cookies.Append("ASP.NET_SessionId", "");
        }
        private ClaimsPrincipal AdminPrincipals(SignInViewModel model, Admin user)
        {
            var userData = JsonConvert.SerializeObject(model);

            var claims = new List<Claim> {
                new Claim("UserId",user.Id.ToString()),
                new Claim (ClaimTypes.NameIdentifier,model.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,UserType.Admin.ToString()),
                new Claim (ClaimTypes.Name, user.Name),
                //new Claim (ClaimTypes.CookiePath, $"/{Variables.AdminPanelCookiePath}"),
               // new Claim (ClaimTypes.Role,Variables.adminer),
                new Claim (ClaimTypes.UserData, userData)

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principals = new ClaimsPrincipal(identity);
            return principals;
        }
        protected Task SignAdminInAsync(SignInViewModel model,Admin user)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var probs = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1)
            };
            /*await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions
            .SignInAsync(HttpContext, "StudentScheme", StudentPrincipals(email, name, id), probs);*/
            LogoutAdmin().Wait();
            return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, AdminPrincipals(model, user), probs);
        }
        #endregion
    }
}
