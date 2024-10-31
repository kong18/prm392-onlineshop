using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.FileUpload
{
    public class FileUploadService
    {
        private readonly FirebaseConfig _firebaseConfig;

        public FileUploadService(FirebaseConfig firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var jwtToken = await GenerateJwtTokenAsync();

                var firebaseStorage = new FirebaseStorage(
                    _firebaseConfig.StorageBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(jwtToken)
                    });

                var task = firebaseStorage
                    .Child("PRM392") // Ensure the path exists
                    .Child(fileName)
                    .PutAsync(fileStream);

                var downloadUrl = await task;

                return downloadUrl;
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        private async Task<string> GenerateJwtTokenAsync()
        {
            var jsonCredentials = $@"
          {{
        ""type"": ""{_firebaseConfig.Type}"",
        ""project_id"": ""{_firebaseConfig.ProjectId}"",
        ""private_key_id"": ""{_firebaseConfig.PrivateKeyId}"",
        ""private_key"": ""{_firebaseConfig.PrivateKey.Replace("\\n", "\n")}"",
        ""client_email"": ""{_firebaseConfig.ClientEmail}"",
        ""client_id"": ""{_firebaseConfig.ClientId}"",
        ""auth_uri"": ""{_firebaseConfig.AuthUri}"",
        ""token_uri"": ""{_firebaseConfig.TokenUri}"",
        ""auth_provider_x509_cert_url"": ""{_firebaseConfig.AuthProviderX509CertUrl}"",
        ""client_x509_cert_url"": ""{_firebaseConfig.ClientX509CertUrl}""
    }}";

            var credential = GoogleCredential
                .FromJson(jsonCredentials)
                .CreateScoped(new[] { "https://www.googleapis.com/auth/devstorage.full_control" });

            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            return token;
        }

    }
}
