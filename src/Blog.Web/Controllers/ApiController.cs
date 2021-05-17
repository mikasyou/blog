using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Articles;
using Blog.Application.Commands;
using Blog.Domain.Shared.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers {
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase {
        private readonly ArticleService _articleService;

        public ApiController(ArticleService articleService) {
            _articleService = articleService;
        }


        [HttpPost("comment")]
        public bool PostComment(CreateCommentCommand command) {
            // TODO: 模型绑定自动校验
            if (string.IsNullOrWhiteSpace(command.Name.Trim())) {
                throw new NullReferenceException("请填写一个昵称");
            }

            _articleService.PostComment(command);
            Response.Cookies.Append("name", command.Name, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            Response.Cookies.Append("email", command.Email, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            Response.Cookies.Append("website", command.WebSite,
                new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
            return true;
        }
    }
}