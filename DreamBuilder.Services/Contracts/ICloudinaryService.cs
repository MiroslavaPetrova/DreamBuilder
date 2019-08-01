using Microsoft.AspNetCore.Http;

namespace DreamBuilder.Services.Contracts
{
    public interface ICloudinaryService
    {
        string UploadImage(IFormFile imageFile, string fileName);
    }
}
