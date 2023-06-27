using System.IO;
using System.Threading.Tasks;

namespace AIF.Services
{
    public interface IAWSS3Service
    {
        Task UploadFileAsync(string fileName, Stream fileStream);
    }
}
