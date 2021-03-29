using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Blog.Application.Commands;
using Blog.Application.Queries;
using Blog.Application.Services;
using Blog.Domain.Shared.Utils;
using Blog.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers {
    [Route("")]
    [Route("blog")]
    public class BlogController : Controller {
        private readonly ArticleService _articleService;
        private readonly IArticleQueries _articleQueries;

        public BlogController(ArticleService articleService, IArticleQueries articleQueries) {
            this._articleService = articleService;
            this._articleQueries = articleQueries;
        }


        [HttpPost("postComment")]
        public bool PostComment(CreateCommentCommand command) {
            // 设置头像
            if (!string.IsNullOrEmpty(command.Email)) {
                command.Avatar = DataTools.MakeGravatarImage(command.Email);
            }

            if (string.IsNullOrEmpty(command.Name.Trim())) {
                throw new NullReferenceException("请填写一个昵称");
            }

            _articleService.PostComment(command);
            Response.Cookies.Append("name", command.Name, new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            Response.Cookies.Append("email", command.Email, new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            Response.Cookies.Append("website", command.WebSite,
                new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            return true;
        }


        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 10) {
            var model = _articleQueries.ListArticles(pageIndex, pageSize);
            return View("Index", model);
        }

        [HttpPost("index")]
        public IActionResult IndexAjax(int pageIndex = 1, int pageSize = 10) {
            var model = _articleQueries.ListArticles(pageIndex, pageSize);
            HttpContext.Response.Headers.Add("title", DataTools.MakeWebTitle("首页", true));
            return PartialView("Index", model);
        }


        [HttpGet("Article/{code}/{articleid:int}")]
        public IActionResult Article(ViewArticleCommand command) {
            var model = _articleService.ViewArticle(command);
            ViewBag.Title = DataTools.MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return View("Article", model);
        }

        [HttpPost("Article/{articleid}")]
        public IActionResult ArticleAjax(ViewArticleCommand command) {
            var model = _articleService.ViewArticle(command);
            ViewBag.Title = DataTools.MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return PartialView("Article", model);
        }


        [HttpGet("friends")]
        public IActionResult Friends(int page = 1, int row = 10) {
            ViewBag.Title = DataTools.MakeWebTitle("友情链接");
            return View("Friends");
        }


        [HttpPost("friends")]
        public IActionResult FriendsAjax() {
            HttpContext.Response.Headers.Add("title", DataTools.MakeWebTitle("友情链接", true));
            return PartialView("Friends");
        }


        [HttpGet("article/{code}/{imageName}")]
        public IActionResult ArticleImage(string code, string imageName) {
            var imagePath = Path.Combine("article", code, imageName);
            return File(imagePath, "image/jpeg");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View("Error", new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}