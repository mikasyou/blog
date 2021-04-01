using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
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
    public class BlogController : Controller {
        private readonly ArticleService _articleService;
        private readonly IArticleQueries _articleQueries;

        public BlogController(ArticleService articleService, IArticleQueries articleQueries) {
            this._articleService = articleService;
            this._articleQueries = articleQueries;
        }


        private IActionResult EnhancedView(string viewName, object model = null) {
            Console.WriteLine(HttpContext.Request.Method);
            if (HttpContext.Request.Method.ToUpper() == "POST") {
                return PartialView(viewName, model);
            }
            else {
                return View(viewName, model);
            }
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


        [Route("")]
        [Route("index")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 10) {
            var model = _articleQueries.ListArticles(pageIndex, pageSize);
            HttpContext.Response.Headers.Add("title", DataTools.MakeWebTitle("首页", true));
            return EnhancedView("Index", model.Items);
        }


        [Route("article/{code}/{articleId:int}")]
        public IActionResult Article(int articleId) {
            var command = new ViewArticleCommand(articleId, "TODO");
            var model = _articleService.ViewArticle(command);
            ViewBag.Title = DataTools.MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return EnhancedView("Article", model);
        }

        [Route("friends")]
        public IActionResult Friends(int page = 1, int row = 10) {
            ViewBag.Title = DataTools.MakeWebTitle("友情链接");
            HttpContext.Response.Headers.Add("title", DataTools.MakeWebTitle("友情链接", true));
            return EnhancedView("Friends");
        }


        [Route("article/{code}/{imageName}")]
        public IActionResult ArticleImage(string code, string imageName) {
            var imagePath = Path.Combine("article", code, imageName);
            return EnhancedView(imagePath, "image/jpeg");
        }


        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return EnhancedView("_Error404",
                new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}