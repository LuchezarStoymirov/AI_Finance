using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace AIF.Services
{
    public class AWSS3Service : IAWSS3Service
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;

        public AWSS3Service(string accessKey, string secretKey, string bucketName, string regionName)
        {
            _bucketName = bucketName;
            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(regionName));
        }

        public async Task UploadFileAsync(string fileName, Stream fileStream)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = "text/csv"
            };

            await _s3Client.PutObjectAsync(putRequest);
        }
    }
}
