using AutoMapper;
using Blog.Application.Articles.Models;
using Blog.Infrastructure.Records;

namespace Blog.Infrastructure.Mappers {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<ArticleRecord, ArticleIndexData>();
        }
    }
}