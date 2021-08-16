using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.WebAPI.Helpers;
using Interact.GateInvitations.WebAPI.Infrastructure.Extensions;
using Interact.GateInvitations.WebAPI.ViewModels.Customer;
using Interact.GateInvitations.WebAPI.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Controllers
{
 
    public class AuthController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ISecurityKeeperService _securityKeeperService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, ICustomerService customerService, ISecurityKeeperService securityKeeperService, IUserService userService)
        {
            _logger = logger;
            _customerService = customerService;
            _securityKeeperService = securityKeeperService;
            _userService = userService;
        }

        [HttpPost("customer/register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = model.ToEntity<Customer>();
            await _customerService.RegisterCustomer(entity);
            return Ok(entity);
        }
        [HttpPost("securitykeeper/register")]
        public async Task<IActionResult> RegisterSecurityKeeper([FromBody] SecurityKeeperRegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = model.ToEntity<SecurityKeeper>();
            await _securityKeeperService.Register(entity);
            return Ok(entity);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUser([FromBody] SignInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var validateCredentials =await _userService.CheckForUserCredintialsIsValidAsync(model.Username, model.Password,model.UserType);
            if (!validateCredentials.isValid)
            {
                return BadRequest(new { mess="Provided username or password is invalid"});
            }
            var responseToken = JwtHelper.CreateAccessToken(validateCredentials.validatedUser);
            return Ok(responseToken);
        }
    }
}
