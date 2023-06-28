
namespace AIF.Services
{
    public interface IS3Service
    {
        Task UploadFileAsync(byte[] fileBytes);
    }
}