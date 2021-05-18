using AutoMapper;
using Blog.Application.Articles.Models;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Utils;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Records;

namespace Blog.Infrastructure.Mappers {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<ArticleRecord, ArticleIndexData>();
            CreateMap<Article, ArticleRecord>();
            CreateMap<CommentRecord, ArticleComment>()
               .ConstructUsing(it => new ArticleComment(
                        DataTools.MakeGravatarImage(it.Email),
                        it.WebSite,
                        it.Name,
                        it.Email,
                        it.Content,
                        it.Id,
                        it.TargetId
                    )
                );
        }
    }
}