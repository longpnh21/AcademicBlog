using Application.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FirebaseUploadService : IUploadService
    {
        private const string API_KEY = "AIzaSyAYMQIuzDvhH0snG9fog2vhYrcm3oduB3g";
        private const string BUCKET = "academicblog-cb0c8.appspot.com";
        private const string AUTH_EMAIL = "administrator@academicblog.com";
        private const string AUTH_PASSWORD = "~d[3f6mz)yxx'D=y";

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
