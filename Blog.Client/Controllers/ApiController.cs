using System;
using System.Security.Cryptography;
using System.Text;
using Blog.Application.Commands;
using Blog.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Client.Controllers {
    [Route("api")]
    [ApiController]
    public class BlogController : ControllerBase {
        private readonly ArticleService _articleService;

        public BlogController(ArticleService articleService) {
            _articleService = articleService;
        }


        private const string GravatarUrl = "https://www.gravatar.com/avatar/";

        private static string GetGravatarImage(string email) {
            var md5 = MD5.Create().ComputeHash(Encoding.Default.GetBytes(email.ToLower()));
            var hash = BitConverter.ToString(md5).Replace("-", "").ToLower();
            return GravatarUrl + hash;
        }


        [HttpPost("postComment")]
        public bool PostComment(CreateCommentCommand command) {
            // 设置头像
            if (!string.IsNullOrEmpty(command.Email))
                command.Avatar = GetGravatarImage(command.Email);
            if (string.IsNullOrEmpty(command.Name.Trim()))
                throw new NullReferenceException("请填写一个昵称");

            _articleService.PostComment(command);
            Response.Cookies.Append("name", command.Name, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            Response.Cookies.Append("email", command.Email, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            Response.Cookies.Append("website", command.WebSite,
                                    new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            return true;
        }
    }
}