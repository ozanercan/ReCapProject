﻿using Core.Utilities.Results;
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

                CreateIfNoFolder(fullFolderPath);

                string fileExtension = GetFileExtension(formFiles);

                string imageName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() : fileName;

                imageName += fileExtension;

                string fullPath = string.Join("/", fullFolderPath, imageName);

                using (FileStream fileStream = File.Create(fullPath))
                {
                    await formFiles.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return new SuccessFileResult(shortPath: string.Join(@"/", uploadFolderName, imageFolderName, imageName), fullPath: fullPath, imageName);
                }
            }
            catch (Exception ex)
            {
                return new ErrorFileResult();
            }
        }

        /// <summary>
        /// FilePath Example: uploads/images/90070018-dc74-4209-a99b-e18f7949daa3.jfif
        /// </summary>
        public static IFileResult FileRemove(string filePath)
        {
            try
            {
                string fileRemovePath = string.Join("/", _webHostEnvironment.ContentRootPath, filePath);

                if (!File.Exists(fileRemovePath))
                    return new ErrorFileResult();

                File.Delete(fileRemovePath);
                return new SuccessFileResult();
            }
            catch (Exception ex)
            {
                return new ErrorFileResult();
            }
        }

        private static void CreateIfNoFolder(string fullFolderPath)
        {
            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);
        }
        private static string GetFileExtension(IFormFile formFiles)
        {
            FileInfo fileInfo = new FileInfo(formFiles.FileName);

            string fileExtension = fileInfo.Extension;
            return fileExtension;
        }
    }
}
