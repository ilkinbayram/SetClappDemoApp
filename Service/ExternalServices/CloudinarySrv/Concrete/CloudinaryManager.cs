using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.ExternalServices.Cloudinary;

namespace Service.ExternalServices.CloudinarySrv.Concrete
{
    public class CloudinaryManager : ICloudinaryService
    {
        private readonly string _cloudName, _apiKey, _apiSecret;
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryManager(string cloudName, string apiKey, string apiSecret)
        {
            var account = new Account(cloudName, apiKey, apiSecret);

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }

        public CloudinaryManager(IOptions<CloudConfig> config)
        {
            _cloudName = CustomCryptor.GetCleanData(config.Value.CLOUDINARYCLOUDNAME);
            _apiKey = CustomCryptor.GetCleanData(config.Value.CLOUDINARYAPIKEY);
            _apiSecret = CustomCryptor.GetCleanData(config.Value.CLOUDINARYAPISECRET);

            var account = new Account(_cloudName, _apiKey, _apiSecret);

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }

        public string BuildUrl(string publicId, string crop = "fill", int width = 150, int height = 150)
        {
            return _cloudinary.Api.Url.Secure(true)
                .Transform(new Transformation().Width(width).Height(height).Crop(crop))
                .BuildUrl(publicId);
        }

        public string BuildUrl(string publicId)
        {
            return _cloudinary.Api.Url.Secure(true).BuildUrl(publicId);
        }

        public string BuildUrlVideo(string publicId)
        {
            return _cloudinary.Api.UrlVideoUp.Secure(true).BuildUrl(publicId);
        }

        public async Task DeleteAsync(string publicId)
        {
            await _cloudinary.DeleteResourcesAsync(publicId);
        }

        public async Task<CloudResult> UploadImageAsync(IFormFile file, string mainFolderPath, string imageFolderPath)
        {
            string savedFilePath = await StoreToLocalAsync(file, mainFolderPath, imageFolderPath);
            var result = new CloudResult();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(savedFilePath),
                UniqueFilename = true,
                Folder = $"{mainFolderPath}/{imageFolderPath}/",
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            result.PulicId = uploadResult.PublicId;
            result.AccessUri = _cloudinary.Api.Url.Secure(true).BuildUrl(uploadResult.PublicId);

            RemoveFromLocal(savedFilePath);

            return result;
        }

        public async Task<CloudResult> UploadVideoAsync(IFormFile file, string mainFolderPath, string videoFolderPath)
        {
            string savedFilePath = await StoreToLocalAsync(file, mainFolderPath, videoFolderPath);
            var result = new CloudResult();

            var uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(savedFilePath),
                UniqueFilename = true,
                Overwrite = true,
                Folder = $"{mainFolderPath}/{videoFolderPath}/"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            result.PulicId = uploadResult.PublicId;
            result.AccessUri = _cloudinary.Api.UrlVideoUp.Secure(true).BuildUrl(uploadResult.PublicId);

            RemoveFromLocal(savedFilePath);

            return result;
        }

        public async Task<CloudResult> UploadResizedImageAsync(IFormFile file, string mainFolderPath, string imageFolderPath, int width, int height, string crop)
        {
            string savedFilePath = await StoreToLocalAsync(file, mainFolderPath, imageFolderPath);
            var result = new CloudResult();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(savedFilePath),
                UniqueFilename = true,
                Folder = $"{mainFolderPath}/{imageFolderPath}/",
                Transformation = new Transformation().Width(width).Height(height).Crop(crop)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            result.PulicId = uploadResult.PublicId;
            result.AccessUri = _cloudinary.Api.Url.Secure(true).BuildUrl(uploadResult.PublicId);
            RemoveFromLocal(savedFilePath);

            return result;
        }

        public async Task<CloudResult> UploadFromUrlAsync(string url, string mainFolderPath, string imageFolderPath)
        {
            var result = new CloudResult();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(url),
                UniqueFilename = true,
                Folder = $"{mainFolderPath}/{imageFolderPath}/"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            result.PulicId = uploadResult.PublicId;
            result.AccessUri = _cloudinary.Api.Url.Secure(true).BuildUrl(uploadResult.PublicId);
            return result;
        }

        private async Task<string> StoreToLocalAsync(IFormFile file, string mainFolderPath, string fileFolderPath)
        {
            CreateFolderIfNotExist(mainFolderPath, fileFolderPath);

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cloudfilecontainer", mainFolderPath, fileFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }

        private void RemoveFromLocal(string fileLocalPath)
        {
            if (File.Exists(fileLocalPath))
                File.Delete(fileLocalPath);
        }

        private void CreateFolderIfNotExist(string mainFolderPath, string fileFolderPath)
        {
            if (!Directory.Exists($"wwwroot/cloudfilecontainer/{mainFolderPath}"))
                Directory.CreateDirectory($"wwwroot/cloudfilecontainer/{mainFolderPath}");
            if (!Directory.Exists($"wwwroot/cloudfilecontainer/{mainFolderPath}/{fileFolderPath}"))
                Directory.CreateDirectory($"wwwroot/cloudfilecontainer/{mainFolderPath}/{fileFolderPath}");
        }

        private void CreateFolderIfNotExist(string folderName)
        {
            if (!Directory.Exists($"wwwroot/cloudfilecontainer/{folderName}"))
                Directory.CreateDirectory($"wwwroot/cloudfilecontainer/{folderName}");
        }
    }
}
