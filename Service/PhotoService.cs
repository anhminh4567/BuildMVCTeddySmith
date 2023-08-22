using BuildMVCTeddySmith.Helpers;
using BuildMVCTeddySmith.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.IO;

namespace BuildMVCTeddySmith.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> settings)
        {
            var acc = new Account(
               settings.Value.CloudName,
               settings.Value.ApiKey,
               settings.Value.ApiSecret
            );
            _cloudinary = new Cloudinary( acc );
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0) {
                using var stream = file.OpenReadStream() ;
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new
                    Transformation().Height(500).Width(500).Crop("fill").Gravity(CloudinaryDotNet.Gravity.Face),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
