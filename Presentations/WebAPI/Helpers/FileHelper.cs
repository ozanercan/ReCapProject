using Core.Utilities.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class FileHelper
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private static string uploadFolderName = "uploads";
        private static string imageFolderName = "images";

        public static void Initialize(IWebHostEnvironment webHostEnvironment)
        {
            if (_webHostEnvironment == null)
                _webHostEnvironment = webHostEnvironment;
        }

        public static async Task<IFileResult> ImageUploadAsync(IFormFile formFiles, string fileName = "")
        {
            try
            {

                string fullFolderPath = string.Join(@"\", _webHostEnvironment.ContentRootPath, uploadFolderName, imageFolderName);

                if (!Directory.Exists(fullFolderPath))
                    Directory.CreateDirectory(fullFolderPath);

                string fileExtension = GetFileExtension(formFiles);

                string imageName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() : fileName;

                imageName += fileExtension;

                string fullPath = fullFolderPath + imageName;

                using (FileStream fileStream = File.Create(fullPath))
                {
                    await formFiles.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return new SuccessFileResult(shortPath: string.Join(@"\", uploadFolderName, imageFolderName, imageName), fullPath: fullPath, imageName);
                }
            }
            catch (Exception ex)
            {
                return new ErrorFileResult();
            }
        }

        private static string GetFileExtension(IFormFile formFiles)
        {
            FileInfo fileInfo = new FileInfo(formFiles.FileName);

            string fileExtension = fileInfo.Extension;
            return fileExtension;
        }
    }
}
