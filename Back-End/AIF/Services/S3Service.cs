using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
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

        public async Task UploadFileAsync(byte[] fileBytes, string fileExtension)
        {
            string folderName = DateTime.Now.ToString("yyyyMMdd");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;

            await CreateFolderIfNotExistsAsync(folderName);

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(memoryStream, _bucketName, $"{folderName}/{fileName}");
            }
        }

        public async Task<List<string>> GetAllFilesAsync()
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName
            };

            var response = await _s3Client.ListObjectsV2Async(request);
            var fileList = new List<string>();

            foreach (var file in response.S3Objects)
            {
                fileList.Add(file.Key);
            }

            return fileList;
        }

        public async Task<byte[]> DownloadFileAsync(string filename, string fileExtension)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = filename
            };

            var response = await _s3Client.GetObjectAsync(request);
            var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private async Task CreateFolderIfNotExistsAsync(string folderName)
        {
            var folderKey = $"{folderName}/";
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = folderKey,
                ContentBody = string.Empty
            };

            await _s3Client.PutObjectAsync(putRequest);
        }
    }
}
