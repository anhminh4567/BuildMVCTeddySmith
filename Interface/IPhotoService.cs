using CloudinaryDotNet.Actions;

namespace BuildMVCTeddySmith.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId );
    }
}
