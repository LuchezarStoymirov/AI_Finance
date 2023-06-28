using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AIF.Services
{
    public class S3Service : IS3Service
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;

        public S3Service(string accessKey, string secretKey, string bucketName, RegionEndpoint region)
        {
            _bucketName = bucketName;
            _s3Client = new AmazonS3Client(accessKey, secretKey, region);
        }

        public async Task UploadFileAsync(byte[] fileBytes)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".dat";
            using (var memoryStream = new MemoryStream(fileBytes))
            {
                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(memoryStream, _bucketName, fileName);
            }
        }
    }

}
