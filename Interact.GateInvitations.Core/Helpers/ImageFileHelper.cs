using Interact.GateInvitations.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interact.GateInvitations.Core.Helpers
{
    public class ImageFileHelper
    {
        public class ImgResponseModel
        {
            public bool Status { get; set; } = false;
            public string errorMess { get; set; }
            public string ImagePath { get; set; }

        }
        public static ImgResponseModel ValidateAndSaveImage(IFormFile imgFile, object Id)
        {
            var response = new ImgResponseModel();
            if (imgFile == null || imgFile.Length == 0)
            {
                response.errorMess = "Image file is not valid";
                return response;
            }
            string imgExt = Path.GetExtension(imgFile.FileName).Replace(".", string.Empty);
            var supportedTypes = new string[] { "png", "jpg", "jpeg", "gif", "PNG", "JPG", "GIF", "JPEG" };
            //not valid extension
            if (!supportedTypes.Contains(imgExt.Replace(".", string.Empty)))
            {
                response.errorMess = "Image file extension is not supported";
                return response;
            }
                
            try
            { //delete old image if exists
                var file = imgFile.OpenReadStream();
                if (file.Length > 0)
                {
                    var imgPath =Utility.InvitationsImagesPath() + $@"/{Id}.{imgExt}";
                    DeleteImgFile(imgPath);
                    using (FileStream fs =File.Create(imgPath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    response.ImagePath =
                        imgPath.Replace(AppServiceProvider.GetService<IHostingEnvironment>().WebRootPath, "")
                        .Replace(@"\", "/");
                }
            }
            catch (Exception ex)
            {
                response.errorMess = ex.Message;
                return response;
            }
            response.Status = true;
            return response;
        }
        public static ImgResponseModel ValidateAndSaveAttachment(IFormFile imgFile, object Id)
        {
            var response = new ImgResponseModel();
            if (imgFile == null || imgFile.Length == 0)
            {
                response.errorMess = "Image file is not valid";
                return response;
            }
            string imgExt = Path.GetExtension(imgFile.FileName).Replace(".", string.Empty);
            var supportedTypes = new string[] { "png", "jpg", "jpeg", "gif", "PNG", "JPG", "GIF", "JPEG" };
            //not valid extension
            if (!supportedTypes.Contains(imgExt.Replace(".", string.Empty)))
            {
                response.errorMess = "Image file extension is not supported";
                return response;
            }

            try
            { //delete old image if exists
                var file = imgFile.OpenReadStream();
                if (file.Length > 0)
                {
                    var imgPath = Utility.AttatchmentsImagesPath() + $@"/{Id}.{imgExt}";
                    DeleteImgFile(imgPath);
                    using (FileStream fs = File.Create(imgPath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    response.ImagePath =
                        imgPath.Replace(AppServiceProvider.GetService<IHostingEnvironment>().WebRootPath, "")
                        .Replace(@"\", "/");
                }
            }
            catch (Exception ex)
            {
                response.errorMess = ex.Message;
                return response;
            }
            response.Status = true;
            return response;
        }
        public static void DeleteImgFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        public static void DeleteImgsFiles(params string[] paths)
        {
            Array.ForEach(paths, path =>
            {
                if (File.Exists(path))
                    File.Delete(path);
            });
        }
    }
}
