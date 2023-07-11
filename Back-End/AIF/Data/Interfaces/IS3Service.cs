namespace AIF.Services
{
    public interface IS3Service
    {
        Task<List<string>> GetAllFilesAsync();
        Task UploadFileAsync(byte[] fileBytes, string fileExtension);
        Task<byte[]> DownloadFileAsync(string filename, string fileExtension);
        Task DeleteFileAsync(string filename);
        Task DeleteFolderAsync(string folderName);
        Task<string> UpdateProfilePictureAsync(int userId, byte[] newPictureBytes, string fileExtension);
    }
}
