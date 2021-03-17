using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Blog.Application.Commands;
using Blog.Application.Queries;
using Blog.Application.Services;
using Blog.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Client.Controllers {
    public class BlogController : Controller {
        private readonly ArticleService articleService;
        private readonly IArticleQueries articleQueries;

        public BlogController(ArticleService articleService, IArticleQueries articleQueries) {
            this.articleService = articleService;
            this.articleQueries = articleQueries;
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

            articleService.PostComment(command);
            Response.Cookies.Append("name", command.Name, new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            Response.Cookies.Append("email", command.Email, new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            Response.Cookies.Append("website", command.WebSite,
                new CookieOptions {Expires = DateTime.Now.AddMonths(1)});
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="urlEncoded"></param>
        /// <returns></returns>
        private static string MakeWebTitle(string title, bool urlEncoded = false) {
            if (urlEncoded)
                return WebUtility.UrlEncode(title + " | Kaakira");

            return title + " | Kaakira";
        }


        [HttpGet("index")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 10) {
            var model = articleQueries.ListArticles(pageIndex, pageSize);
            return View("Index", model);
        }


        /// <summary>
        /// ajax
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("index")]
        public IActionResult IndexAjax(int pageIndex = 1, int pageSize = 10) {
            var model = articleQueries.ListArticles(pageIndex, pageSize);
            HttpContext.Response.Headers.Add("title", MakeWebTitle("首页", true));
            return PartialView("Index", model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleid"></param>
        /// <returns></returns>
        [HttpGet("Article/{code}/{articleid:int}")]
        public IActionResult Article(ViewArticleCommand command) {
            var model = articleService.ViewArticle(command);
            ViewBag.Title = MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return View("Article", model);
        }


        /// <summary>
        /// ajax
        /// </summary>
        /// <param name="page"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost("Article/{articleid}")]
        public IActionResult ArticleAjax(ViewArticleCommand command) {
            var model = articleService.ViewArticle(command);
            ViewBag.Title = MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return PartialView("Article", model);
        }


        [HttpGet("friends")]
        public IActionResult Friends(int page = 1, int row = 10) {
            ViewBag.Title = MakeWebTitle("友情链接");
            return View("Friends");
        }


        /// <summary>
        /// ajax
        /// </summary>
        /// <param name="page"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost("friends")]
        public IActionResult FriendsAjax() {
            HttpContext.Response.Headers.Add("title", MakeWebTitle("友情链接", true));
            return PartialView("Friends");
        }


        [HttpGet("article/{code}/{imageName}")]
        public IActionResult ArticleImage(string code, string imageName) {
            var imagePath = Path.Combine("article", code, imageName);
            return File(imagePath, "image/jpeg");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View("_Error404",
                new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}