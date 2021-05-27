using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Blog.Application.Articles;
using Blog.Application.Articles.Models;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Collections;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly BlogDatabaseContext context;
        private readonly IMapper mapper;

        public ArticleQueries(BlogDatabaseContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }


        public Page<ArticleIndexData> FindArticles(PagingLimit paging) {
            IQueryable<Article> sql = context.Articles;
            if (paging != null) {
                sql = sql.Skip(paging.Offset).Take(paging.Limit);
            }

            var total = context.Articles.Count();
            var items = sql.Select(it => new ArticleIndexData {
                                    Id = it.Id,
                                    Code = it.Code,
                                    Title = it.Title,
                                    SubTitle = it.SubTitle,
                                    Summary = it.Summary,
                                    Tags = it.Tags ?? new List<Tag>(),
                                    CommentCounts = it.Comments.Count,
                                    CreateDate = it.CreateDate,
                                    UpdateDate = it.UpdateDate,
                                }
                            )
                           .ToList();
            return new(items, total);
        }
    }
}