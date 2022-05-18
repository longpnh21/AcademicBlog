using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder = "files");
    }
}
