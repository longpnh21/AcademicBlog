using Application.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FirebaseUploadService : IUploadService
    {
        private const string API_KEY = "";
        private const string BUCKET = "";
        private const string AUTH_EMAIL = "";
        private const string AUTH_PASSWORD = "";

        public async Task<string> UploadFileAsync(IFormFile file, string folder = "files")
        {
            string link = string.Empty;
            try
            {
                using (var stream = file.OpenReadStream())
                {

                    var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
                    var signIn = await auth.SignInWithEmailAndPasswordAsync(AUTH_EMAIL, AUTH_PASSWORD);

                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        BUCKET,
                        new FirebaseStorageOptions
                        {

                            AuthTokenAsyncFactory = () => Task.FromResult(signIn.FirebaseToken),
                            ThrowOnCancel = true
                        })
                        .Child(folder)
                        .Child(Path.GetRandomFileName() + Path.GetExtension(file.FileName))
                        .PutAsync(stream);
                    link = await task;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return link;
        }
    }
}
