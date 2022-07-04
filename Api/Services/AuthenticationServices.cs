using Api.Helpers;
using Api.Models;
using Application.Response.Base;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(IOptions<AppSettings> appSettings, UserManager<User> userManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public async Task<Response<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new NullReferenceException("Not found account");
                }
                if (!(await _userManager.CheckPasswordAsync(user, request.Password)))
                {
                    throw new NullReferenceException("Not valid login information");
                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = await _userManager.GetClaimsAsync(user);
                var token = GenerateJwtToken(user, claims);
                var response = new AuthenticationResponse
                {
                    Email = user.Email,
                    JwtToken = tokenHandler.WriteToken(token),
                    ExpiredAt = token.ValidTo
                };
                return new Response<AuthenticationResponse>(response)
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (NullReferenceException ex)
            {
                return new Response<AuthenticationResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
        }


        private SecurityToken GenerateJwtToken(User user, IList<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claimsList = new Dictionary<string, object>();
            foreach (var claim in claims)
            {
                claimsList.Add(claim.Type, claim.Value);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Claims = claimsList,
                Issuer = _appSettings.ValidIssuer,
                Audience = _appSettings.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
