using Application.Commands.Users;
using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISendMailService _sendMailService;

        public CreateUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ISendMailService sendMailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sendMailService = sendMailService;
        }

        public async Task<Response<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                var user = new User() { UserName = request.Email.Split('@')[0], Email = request.Email, FullName = request.FullName };
                if (await _userManager.FindByEmailAsync(request.Email) is not null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var userRole = await _roleManager.FindByNameAsync(request.Role);
                if (userRole is null)
                {
                    throw new ArgumentNullException("Not found role: " + request.Role);
                }

                var password = CreatePassword(8);
                var result = await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, userRole.Name);

                var content = new MailContent()
                {
                    To = request.Email,
                    Subject = "Access information to Academic Blog",
                    Body = CreateBodyMessage(request.Email, password)
                };

                await _sendMailService.SendMail(content);

                response = new Response<UserResponse>(AcademicBlogMapper.Mapper.Map<UserResponse>(user))
                {
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (ArgumentNullException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890|@!$%*";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private string CreateBodyMessage(string email, string password)
        {
            return "<div lang=\"EN-US\" link=\"blue\" vlink=\"#954F72\"><div class=\"m_-863817368153641209WordSection1\"><p><span style=\"font-size:13.5pt;color:black\">Thư mời,<u></u><u></u></span></p><p><span style=\"font-size:13.5pt;color:black\"><u></u>&nbsp;<u></u></span></p><p style=\"line-height:150%\"><span style=\"font-size:13.5pt;line-height:150%;color:black\">Đây là thư gửi tài khoản để em đăng nhập AcademicBlog </span><b></p><table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse:collapse;border:none\"><tbody><tr style=\"height:24.6pt\"><td width=\"100\" style=\"width:74.7pt;border:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;height:24.6pt\"><p><span style=\"font-size:13.5pt\">Username<u></u><u></u></span></p></td><td width=\"213\" style=\"width:159.75pt;border:solid windowtext 1.0pt;border-left:none;padding:0in 5.4pt 0in 5.4pt;height:24.6pt\"><p align=\"center\" style=\"text-align:center\"><span style=\"font-size:13.5pt\">" + email + "</span><span style=\"font-size:13.5pt\"><u></u><u></u></span></p></td></tr><tr style=\"height:24.6pt\"><td width=\"100\" style=\"width:74.7pt;border:solid windowtext 1.0pt;border-top:none;padding:0in 5.4pt 0in 5.4pt;height:24.6pt\"><p><span style=\"font-size:13.5pt\">Password<u></u><u></u></span></p></td><td width=\"213\"style=\"width:159.75pt;border-top:none;border-left:none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;padding:0in 5.4pt 0in 5.4pt;height:24.6pt\"><p align=\"center\" style=\"text-align:center\"><span style=\"font-size:13.5pt\">" + password + "</span><span style=\"font-size:13.5pt\"><u></u><u></u></span></p></td></tr></tbody></table></div></div>";
        }
    }
}
