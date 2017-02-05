using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegamiManager.Models.MegamiModels;
using Microsoft.AspNetCore.Http;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using Amazon.Runtime;
using Amazon;

namespace MegamiManager.Repositories
{
    /// <remarks>
    /// https://docs.aws.amazon.com/ja_jp/AmazonS3/latest/dev/UploadObjSingleOpNET.html
    /// </remarks>
    public class SakuraObjectStorageImageRepository : IImageRepository
    {
        private static readonly string ENDPOINT_BASE = "b.sakurastorage.jp/";
        private static readonly string CHACE_BASE = "c.sakurastorage.jp/";

        private IAmazonS3 client;
        private string bucketName;
        public SakuraObjectStorageImageRepository(string bucketName, string key, string secret)
        {
            // デフォルトの AWS4 に対応していない
            AWSConfigsS3.UseSignatureVersion4 = false;
            client = new AmazonS3Client(
                new BasicAWSCredentials(key, secret),
                new AmazonS3Config()
                {
                    ServiceURL = "https://" + ENDPOINT_BASE,
                    // XXX この設定は間違っているか無視される
                    SignatureMethod = SigningAlgorithm.HmacSHA1,
                    SignatureVersion = "2"
                });
            this.bucketName = bucketName;
        }

        private string BuildPublicUri(string key)
        {
            return $"https://{bucketName}.{CHACE_BASE}{key}";
        }
        private string BuildPrivateUri(string key)
        {
            return $"https://{bucketName}.{ENDPOINT_BASE}{key}";
        }
        private void ValidateFile(IFormFile file)
        {
            if (!file.ContentType.StartsWith("image"))
            {
                throw new ArgumentException($"{file.ContentType} is invalid ContentType.");
            }
            if (file.Length > 1024 * 1024 * 10)
            {
                throw new ArgumentException($"{file.Length} is too large Length.");
            }
        }

        public async Task<Image> Create(IFormFile file)
        {
            ValidateFile(file);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = "images/" + fileName;
            var name = Path.GetFileNameWithoutExtension(file.FileName);
            var request = new PutObjectRequest()
            {
                InputStream = file.OpenReadStream(),
                BucketName = bucketName,
                Key = filePath
            };
            var response = await client.PutObjectAsync(request);

            return new Image()
            {
                Key = filePath,
                Name = name,
                PublicUri = BuildPublicUri(filePath),
                PrivateUri = BuildPrivateUri(filePath)
            };
        }

        public Task Update(Image image, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Image image)
        {
            throw new NotImplementedException();
        }

        public Task CreateThumbnail(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
