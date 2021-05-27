using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly BlogDatabaseContext context;
        private readonly IMapper mapper;

        public ArticleRepository(BlogDatabaseContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<Article> GetAsync(int articleId) {
            var order = await context.Articles
                                     .Include(it => it.Comments)
                                     .Where(o => o.Id == articleId)
                                     .SingleOrDefaultAsync();
            return order;
        }

        public Article Add(Article order) {
            return context.Articles.Add(order).Entity;
        }

        public async void Save() {
            await context.SaveEntitiesAsync();
        }
    }
}