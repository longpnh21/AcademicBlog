using System;

namespace Api.Models
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
