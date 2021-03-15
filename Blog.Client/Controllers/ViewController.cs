using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Blog.Application.Commands;
using Blog.Application.Models;
using Blog.Application.Queries;
using Blog.Application.Services;
using Blog.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Client.Controllers {
    public class ViewController : Controller {
        private readonly ArticleService articleService;
        private readonly IArticleQueries articleQueries;

        public ViewController(ArticleService articleService, IArticleQueries articleQueries) {
            this.articleService = articleService;
            this.articleQueries = articleQueries;
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


        /// <summary>
        /// 默认路由
        /// </summary>
        /// <returns></returns>
        [ActionName("index")]
        public IActionResult DefaultIndex() {
            return Index();
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
            ArticleTO model = articleService.ViewArticle(command);
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
            ArticleTO model = articleService.ViewArticle(command);
            ViewBag.Title = MakeWebTitle(model.Title);
            ViewBag.email = Request.Cookies["email"];
            ViewBag.website = Request.Cookies["website"];
            ViewBag.name = Request.Cookies["name"];
            return PartialView("Article", model);
        }


        [HttpGet("friends")]
        public IActionResult Friends(int page = 1, int row = 10) {
            throw new NotImplementedException();
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
                        new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}