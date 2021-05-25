using AutoMapper;
using Blog.Application.Articles.Models;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Utils;
using Blog.Infrastructure.EntityConfigurations;

namespace Blog.Infrastructure.Mappers {
    public class MappingProfile : Profile {
        public MappingProfile() {
        }
    }
}