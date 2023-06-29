
namespace AIF.Services
{
    public interface IS3Service
    {
        Task<List<string>> GetAllFilesAsync();
        Task UploadFileAsync(byte[] fileBytes, string fileExtension);
        Task<byte[]> DonwloadFileAsync(string filename, string fileExtension);
    }
}