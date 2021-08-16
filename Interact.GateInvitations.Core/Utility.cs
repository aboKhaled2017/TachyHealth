using Interact.GateInvitations.Common;
using Interact.GateInvitations.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core
{
    public class Utility
    {
        
        public static string InvitationsImagesPath()
        {
            var envService = AppServiceProvider.GetService<IHostingEnvironment>();
            return Path.Combine(envService.WebRootPath, "Images", "Invitations"); 
        }
        public static string AttatchmentsImagesPath()
        {
            var envService = AppServiceProvider.GetService<IHostingEnvironment>();
            return Path.Combine(envService.WebRootPath, "Images", "Attatchments");
        }
        public static string MapQRCodePath(string fileName)
        {
            var envService = AppServiceProvider.GetService<IHostingEnvironment>();
            //var path = Path.Combine(envService.ContentRootPath, fileUrl);
            return Path.Combine(envService.WebRootPath, "Images", "QRCodes", fileName);
        }
        public static string MapPath(string basePath,string fileUrl)
        {
            var envService = AppServiceProvider.GetService<IHostingEnvironment>();
            //var path = Path.Combine(envService.ContentRootPath, fileUrl);
            return Path.Combine(basePath, fileUrl);
        }
        public static string GetCurrentLoggedUserId()
        {
            var httpContext = AppServiceProvider.GetService<IHttpContextAccessor>();
            return httpContext.HttpContext.User.Identity.IsAuthenticated
                ?httpContext.HttpContext.User.GetLoggedInUserId<string>()
                :null;
        }
    }
}
