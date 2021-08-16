using Interact.GateInvitations.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.AdminPanel.Controllers
{
    [Authorize(policy: AdminConfig.AdminAuthorizationPolicy)]
    public class MainController : Controller
    {
     
    }
}
