using Api.Models;
using Application.Response.Base;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAuthenticationService
    {
        Task<Response<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
    }
}