using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceCommon.Infrastructure.Ultil
{
    public static class Ultil
    {
        public static string GetFullHostUrl(IHttpContextAccessor _httpContextAccessor)
        {
            string schema = _httpContextAccessor.HttpContext.Request.Scheme;
            string domain = _httpContextAccessor.HttpContext.Request.Host.Value;
            return $"{schema}://{domain}";
        }
        public static async Task<string> UploadFileAsync(IFormFile upload ,string wwwRootPath,string folder)
        {
            string wwwRootPathWeb = wwwRootPath.Replace("Ecommerce.Admin", "Ecommerce.Web");
            string fileName = Path.GetFileNameWithoutExtension(upload.FileName);
            string extension = Path.GetExtension(upload.FileName);
            fileName = fileName + DateTime.Now.ToString("yyMMddHHmmss") + extension;
            string path = Path.Combine(wwwRootPath , folder, fileName);
            string pathWeb = Path.Combine(wwwRootPathWeb , folder, fileName);
            //string domainName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await upload.CopyToAsync(fileStream);
            }
            using (var fileStream = new FileStream(pathWeb, FileMode.Create))
            {
                await upload.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public static void DeleteFile(string URLImage, string wwwRootPath,string folder)
        {
            string wwwRootPathWeb = wwwRootPath.Replace("Ecommerce.Admin", "Ecommerce.Web");
            var OldImagePath = Path.Combine(wwwRootPath, folder, URLImage);
            var OldImagePathWeb = Path.Combine(wwwRootPathWeb, folder, URLImage);
            if (System.IO.File.Exists(OldImagePath))
            {
                System.IO.File.Delete(OldImagePath);
            }
            if (System.IO.File.Exists(OldImagePathWeb))
            {
                System.IO.File.Delete(OldImagePathWeb);
            }
        }
    }
}
