using backend.Core.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.AspNetCore.Http;

namespace backend.Helper
{
    public static class FileHelper
    {
        private static Cloudinary _cloudinary = null;
        public static void CreateCloudinaryKey(IServiceCollection services)
        {
            if (_cloudinary is null)
            {
                DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
                Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
                cloudinary.Api.Secure = true;
                _cloudinary = cloudinary;
            }
        }
        public async static Task<string> UploadImageAsync(IFormFile? file, long bytes = 5000000)
        {
            if (file is null || file.Length == 0 || file.Length >= bytes)
            {
                throw new ValidationException("Eroros process image imager bigger 5mb");
            }

            var filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,

            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            FileInfo currentFile = new(filePath);
            currentFile.Delete();
            return uploadResult.SecureUrl.ToString();
        }
        //public async Task RemoveImageAsync(string url) => await _cloudinary.DestroyAsync(new DeletionParams
        //{
            
        //});
    }
}
