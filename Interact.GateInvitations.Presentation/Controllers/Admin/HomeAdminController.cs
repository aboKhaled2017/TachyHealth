using Interact.GateInvitations.Common.Constants;
using Interact.GateInvitations.Core;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Helpers;
using Interact.GateInvitations.Core.Services;
using Interact.GateInvitations.WebAPI.Infrastructure.Extensions;
using Interact.GateInvitations.WebAPI.ViewModels.Admin;
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
    [Authorize(Policy = GateAppConfig.AdminPolicy)]
    [Route("api/admin")]
    public class HomeAdminController:BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IInvitationService _invitationService;
        private readonly ILogger<AuthController> _logger;

        public HomeAdminController(ILogger<AuthController> logger, IAdminService adminService, IInvitationService invitationService = null)
        {
            _logger = logger;
            _adminService = adminService;
            _invitationService = invitationService;
        }

        [HttpGet("invitations")]
        public async Task<IActionResult> GetAllInvitations()
        {
            var data=await _adminService.GetAllInvitationsAsync<ShowInvitationForAdminViewModel>();
            return Ok(data);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var data =await _adminService.GetAllCustomersAsync<ShowCustomerForAdminViewModel>();
            return Ok(data);
        }

        [HttpGet("securitykeepers")]
        public async Task<IActionResult> GetAllSecurityKeepers()
        {
            var data =await _adminService.GetAllSecurityKeepersAsync<ShowSecurityKeeperForAdminViewModel>();
            return Ok(data);
        }
        [HttpPut("set-customer-status/{customerId}")]
        public async Task<IActionResult> SetCustomerStatus([FromRoute]Guid customerId)
        {
            if (customerId==Guid.Empty) return BadRequest();
            var res = await _adminService.SetCustomerStatus(customerId);
            if (!res.status) return BadRequest(new { g=res.errorMess});
            return NoContent();
        }
        [HttpGet("summary-data")]
        public async Task<IActionResult> GetSummaryDataForAdminDashbord()
        {
            return Ok(await _adminService.GetStaticDataOfAdminDashbord());
        }
    }
}
