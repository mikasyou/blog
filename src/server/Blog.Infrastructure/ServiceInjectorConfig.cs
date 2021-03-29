using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.Queries;
using Blog.Application.Services;
using Blog.Domain.AggregatesModel.Article;
using Blog.Infrastructure.Queries;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure {
    public static class ServiceInjectorConfig {
        public static void AddBlogServices(this IServiceCollection services) {
            services.AddSingleton<ArticleService>();
            services.AddSingleton<IArticleQueries, ArticleQueries>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();
        }
    }
}