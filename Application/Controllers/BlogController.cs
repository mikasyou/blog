using AyaBlog.DaoService;
using AyaBlog.Models;
using AyaEntity.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace AyaBlog.Controllers
{
  public class BlogController : Controller
  {
    readonly IConfiguration appsettings;
    BlogDbService blogService;
    readonly string path;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appsettings"></param>
    public BlogController(IConfiguration appsettings, SqlManager manager, IHostingEnvironment host)
    {
      path = host.WebRootPath;
      this.appsettings = appsettings;
      blogService = manager.UseService<BlogDbService>();
    }




    /// <summary>
    /// 默认路由
    /// </summary>
    /// <returns></returns>
    [ActionName("Test")]
    public IActionResult Test()
    {
      throw new NotImplementedException("测试异常");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="urlEncoded"></param>
    /// <returns></returns>
    private string GetTitle(string title, bool urlEncoded = false)
    {
      if (urlEncoded)
      {
        return WebUtility.UrlEncode(title + " | Kaakira");
      }
      return title + " | Kaakira";
    }




    /// <summary>
    /// 默认路由
    /// </summary>
    /// <returns></returns>
    [ActionName("index")]
    public IActionResult DefaultIndex()
    {
      return Index();
    }

    [HttpGet("index")]
    public IActionResult Index(int page = 1, int row = 10)
    {
      IEnumerable<BlogArticle> model = blogService.GetEntityList<BlogArticle>().OrderByDescending(m => m.Id);
      ViewBag.ArticleTag = blogService.GetSysDict("article_tag");
      ViewBag.Title = GetTitle("首页");
      return View("Index", model);
    }


    /// <summary>
    /// ajax
    /// </summary>
    /// <param name="page"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    [HttpPost("index")]
    public IActionResult IndexAjax(int page = 1, int row = 10)
    {
      IEnumerable<BlogArticle> model = blogService.GetEntityList<BlogArticle>().OrderByDescending(m => m.Id); ;
      ViewBag.ArticleTag = blogService.GetSysDict("article_tag");
      this.HttpContext.Response.Headers.Add("title", GetTitle("首页", true));
      return PartialView("Index", model);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="articleid"></param>
    /// <returns></returns>
    [HttpGet("Article/{code}/{articleid:int}")]
    public IActionResult Article(string code, int articleid)
    {

      BlogArticle model = blogService.GetArticleWithView(new { id = articleid });
      ViewBag.Title = GetTitle(model.Title);

      ViewBag.email = Request.Cookies["email"];
      ViewBag.website = Request.Cookies["website"];
      ViewBag.name = Request.Cookies["name"];
      model.Comments = blogService.GetArticleComments(model.Id, 1);
      ViewBag.ArticleTag = blogService.GetSysDict("article_tag");

      return View("Article", model);
    }




    /// <summary>
    /// ajax
    /// </summary>
    /// <param name="page"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    [HttpPost("Article/{articleid}")]
    public IActionResult ArticleAjax(int articleid)
    {
      BlogArticle model = blogService.GetArticleWithView(new { id = articleid });
      this.HttpContext.Response.Headers.Add("title", this.GetTitle(model.Title, true));
      model.Comments = blogService.GetArticleComments(model.Id, 1);
      ViewBag.email = Request.Cookies["email"];
      ViewBag.ArticleTag = blogService.GetSysDict("article_tag");
      ViewBag.website = Request.Cookies["website"];
      ViewBag.name = Request.Cookies["name"];

      return PartialView("Article", model);
    }


    [HttpGet("friends")]
    public IActionResult Friends(int page = 1, int row = 10)
    {
      IEnumerable<BlogArticle> model = blogService.GetEntityList<BlogArticle>();
      ViewBag.Title = GetTitle("友情链接");
      return View("Friends");
    }


    /// <summary>
    /// ajax
    /// </summary>
    /// <param name="page"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    [HttpPost("friends")]
    public IActionResult FriendsAjax()
    {
      this.HttpContext.Response.Headers.Add("title", GetTitle("友情链接", true));
      return PartialView("Friends");
    }



    [HttpGet("article/{code}/{imageName}")]
    public IActionResult ArticleImage(string code, string imageName)
    {
      string imagePath = Path.Combine("article", code, imageName);
      return File(imagePath, "image/jpeg");
    }



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("_Error404", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }



  }
}
