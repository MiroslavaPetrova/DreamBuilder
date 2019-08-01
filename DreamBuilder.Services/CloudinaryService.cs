using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DreamBuilder.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DreamBuilder.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public string UploadImage(IFormFile imageFile, string fileName)
        {
            byte[] destination;

            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                destination = memoryStream.ToArray();
            }

            UploadResult uploadResult = null;

            using (var memoryStream = new MemoryStream(destination))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "product_images",
                    File = new FileDescription(fileName, memoryStream)
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
