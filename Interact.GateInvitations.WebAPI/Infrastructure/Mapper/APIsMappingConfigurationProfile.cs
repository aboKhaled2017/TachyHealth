using AutoMapper;
using Interact.GateInvitations.Common;
using Interact.GateInvitations.Core.Data;
using Interact.GateInvitations.Core.Helpers;
using Interact.GateInvitations.WebAPI.ViewModels.Admin;
using Interact.GateInvitations.WebAPI.ViewModels.Customer;
using Interact.GateInvitations.WebAPI.ViewModels.Invitation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interact.GateInvitations.WebAPI.Infrastructure.Mapper
{
    public class APIsMappingConfigurationProfile:Profile
    {
        public APIsMappingConfigurationProfile()
        {
            #region Customer Entity

            CreateMap<CustomerRegisterViewModel, Customer>()
             .AfterMap((model, entity) => {
                 entity.User.Username = model.Username;
                 entity.User.Password = model.Password;
             });

            CreateMap<Customer, ShowCustomerForAdminViewModel>()
              .ForMember(e => e.CustomerId, fr => fr.MapFrom(r => r.Id))
              .ForMember(e => e.InvitationsCount, fr => fr.MapFrom(r => r.Invitations.Count))
              .ForMember(e => e.Username, fr => fr.MapFrom(r => r.User.Username))
              .ForMember(e => e.UserStatus, fr => fr.MapFrom(r => r.UserStatus.GetUserStatus()));
            #endregion

            #region SecurityKeeper Entity
            CreateMap<SecurityKeeperRegisterViewModel, SecurityKeeper>()
               .AfterMap((model, entity) =>
               {
                   entity.User.Username = model.Username;
                   entity.User.Password = model.Password;
               });
            CreateMap<SecurityKeeper, ShowSecurityKeeperForAdminViewModel>()
              .ForMember(e => e.SecurityKeeperId, fr => fr.MapFrom(r => r.Id))
              .ForMember(e => e.Username, fr => fr.MapFrom(r => r.User.Username))
              .ForMember(e => e.LoginTries, fr => fr.MapFrom(r => r.InvitationsLogins.Count));
            #endregion

            #region Invitation Entity
            CreateMap<MakeInviteViewModel, Invitation>()
                .AfterMap((model, entity) => {
                    var handleImgFileResponse = ImageFileHelper.ValidateAndSaveImage(model.ImgFile, entity.Id);

                    if (handleImgFileResponse.Status)
                    {
                        entity.ImgUrl = handleImgFileResponse.ImagePath;
                    }
                    else
                    {
                        throw new Exception("Image file is not valid");
                    }
                    var qrcode = QRCodeHelper.GenerateQRCode(model.CustomerId, model.InvitedName);
                    entity.QRCode = qrcode.qrCode;
                    entity.QRCodeImgUrl = qrcode.qrCodeImgUrl;
                });

            CreateMap<Invitation, InvitationDetailsViewModel>()
                .ForMember(e => e.InviteeName, fr => fr.MapFrom(r => r.InvitedName))
                .ForMember(e => e.InviterName, fr => fr.MapFrom(r => r.Customer.User.Username));

            CreateMap<Invitation, ShowInvitationForAdminViewModel>()
             .ForMember(e => e.InviterUsername, fr => fr.MapFrom(r => r.Customer.User.Username))
             .ForMember(e => e.InviterId, fr => fr.MapFrom(r => r.CustomerId))
             .ForMember(e => e.InviteeName, fr => fr.MapFrom(r => r.InvitedName))
             .ForMember(e => e.InviterName, fr => fr.MapFrom(r => r.Customer.User.Username));
            #endregion

            #region InviteeLogin Entity
            CreateMap<InvitationLoginStatusViewModel, InviteeLogin>()
               .AfterMap((model, entity) => {
                   entity.RelatedInvitationId = model.InvitationId;
                   entity.LoginStatus = model.Status;
                   entity.Comment = model.Reason;
               });
            #endregion 
        }
    }
}
