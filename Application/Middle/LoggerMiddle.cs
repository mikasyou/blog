using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Middle
{
  public class LoggerMiddleware
  {

    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      // 记录一下访问者的ip




      // Call the next delegate/middleware in the pipeline
      await _next(context);
    }
  }
}
