using Microsoft.AspNetCore.Http;
using Service.ExternalServices.CloudinarySrv.Concrete;

namespace Service.ExternalServices.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<CloudResult> UploadImageAsync(IFormFile file, string mainFolderPath, string imageFolderPath);
        Task<CloudResult> UploadVideoAsync(IFormFile file, string mainFolderPath, string videoFolderPath);
        Task<CloudResult> UploadResizedImageAsync(IFormFile file, string mainFolderPath, string fileFolderPath, int width, int height, string crop);
        Task<CloudResult> UploadFromUrlAsync(string url, string mainFolderPath, string imageFolderPath);
        Task DeleteAsync(string publicId);


        string BuildUrl(string publicId, string crop = "fill", int width = 150, int height = 150);
        string BuildUrl(string publicId);
        string BuildUrlVideo(string publicId);
    }
}
