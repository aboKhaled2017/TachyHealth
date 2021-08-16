using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Core;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Helpers;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.WebAPI.Infrastructure.Extensions;
using Interact.GateInvitations.WebAPI.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Controllers
{
    [Authorize(Policy = GateAppConfig.CustomerPolicy)]
    public class CustomerController:BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IInvitationService _invitationService;
        private readonly ILogger<AuthController> _logger;

        public CustomerController(ILogger<AuthController> logger, ICustomerService customerService, IInvitationService invitationService)
        {
            _logger = logger;
            _customerService = customerService;
            _invitationService = invitationService;
        }
        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("done");
        }

        [HttpPost("invite")]
        public async Task<IActionResult> MakeInvite([FromForm] MakeInviteViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
           
            var entity = model.ToEntity<Invitation>();
            var c = QRCodeHelper.ReadQRCode(entity.QRCodeImgUrl);
            await _invitationService.MakeInvite(entity);
            return Ok(entity);
        }
    }
}
