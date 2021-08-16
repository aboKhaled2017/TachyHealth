using Interact.GateInvitations.Core;
using Interact.GateInvitations.WebAPI.Attributes.ModelValidationAtttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.ViewModels.Customer
{
    public record MakeInviteViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Customer id is required")]
        [IsValidCustomerId]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Invitee name is required")]
        public string InvitedName { get; set; }
        [Required(ErrorMessage ="Image file is required")]
        [AllowedExtensions(new string[] { "png", "jpg", "jpeg", "gif", "PNG", "JPG", "GIF", "JPEG" })]
        [DataType(DataType.Upload)]
        public IFormFile ImgFile { get; set; }
        //public Guid GetCustomerId()
        //{
        //    return Guid.Parse(CustomerId);
        //}
    }
}
