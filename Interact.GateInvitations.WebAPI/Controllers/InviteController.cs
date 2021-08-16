using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Core;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Helpers;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.WebAPI.Infrastructure.Extensions;
using Interact.GateInvitations.WebAPI.ViewModels.Customer;
using Interact.GateInvitations.WebAPI.ViewModels.Invitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Controllers
{
    [Authorize]
    public class InviteController:BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IInvitationService _invitationService;
        private readonly ISecurityKeeperService _securityKeeperService;
        private readonly ILogger<AuthController> _logger;

        public InviteController(ILogger<AuthController> logger, ICustomerService customerService, IInvitationService invitationService, ISecurityKeeperService securityKeeperService)
        {
            _logger = logger;
            _customerService = customerService;
            _invitationService = invitationService;
            _securityKeeperService = securityKeeperService;
        }

        [Authorize(Policy = GateAppConfig.SecurityKeeperPolicy)]
        [HttpPost("verifyqrcode")]
        public async Task<IActionResult> VerifyQrCode([FromBody]ValidateInviteViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var verifyCode =await _securityKeeperService.ValideQrCodeAsync<InvitationDetailsViewModel>(model.QrCode);
            if (!verifyCode.isValid)
            {
                return NotFound();
            }
            return Ok(verifyCode.InvitationDetails);
        }

        [Authorize(Policy = GateAppConfig.SecurityKeeperPolicy)]
        [HttpPost("login")]
        public async Task<IActionResult> SetInviteLoginStatus([FromBody] InvitationLoginStatusViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var inviteLogin = model.ToEntity<InviteeLogin>();
            inviteLogin.HandlerSecurityKeeperId = LoggedUserId;
            await _securityKeeperService.AddInvitationLoginAsync(inviteLogin);           
            return Ok();
        }
    }
}
