using System;

namespace Application.Response
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public DateTime? ExpiredAt { get; set; }
    }
}
