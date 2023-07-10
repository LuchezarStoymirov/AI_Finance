using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AIF.Data;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;


namespace AIF.Services
{
    public class S3Service : IS3Service
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;
        private readonly IUserRepository _userRepository;

        public S3Service(string accessKey, string secretKey, string bucketName, RegionEndpoint region, IUserRepository userRepository)
        {
            _bucketName = bucketName;
            _s3Client = new AmazonS3Client(accessKey, secretKey, region);
            _userRepository = userRepository;
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

        public async Task DeleteFileAsync(string filename)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = filename
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task DeleteFolderAsync(string folderName)
        {
            var fileList = await GetAllFilesAsync();
            var folderPrefix = $"{folderName}/";

            foreach (var file in fileList)
            {
                if (file.StartsWith(folderPrefix))
                {
                    await DeleteFileAsync(file);
                }
            }
        }

        public async Task<string> UpdateProfilePictureAsync(byte[] newPictureBytes, string fileExtension)
        {
            try
            {
                int userId = GetUserIdFromDatabase(); // Retrieve the user ID from the database

                string folderName = "Profile Pictures";
                string newFileName = $"{userId} Profile Picture{fileExtension}";
                string profilePictureKey = $"{folderName}/{newFileName}";

                var fileList = await GetAllFilesAsync();

                if (fileList.Contains(profilePictureKey))
                {
                    await DeleteFileAsync(profilePictureKey);
                }

                await UploadFileAsync(newPictureBytes, profilePictureKey);

                string newAddress = $"{_bucketName}/{profilePictureKey}";

                await UpdateProfilePictureAddressInDatabase(userId, newAddress);

                return newAddress;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        private int GetUserIdFromDatabase()
        {
            // Replace this with your actual code to retrieve the user ID from the database
            // For example, if you're using Entity Framework, you can do something like:
            var user = _userRepository.GetLoggedInUser(); // Modify this according to your repository method
            if (user != null)
            {
                return user.Id;
            }

            throw new Exception("User not found in the database."); // Throw an exception if user not found
        }

        private async Task UpdateProfilePictureAddressInDatabase(int userId, string newAddress)
        {
            // Replace this with your actual code to update the database with the new profile picture address
            // For example, if you're using Entity Framework, you can do something like:
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.ProfilePictureUrl = newAddress;
                await _userRepository.UpdateAsync(user);
            }
        }

    }
}
