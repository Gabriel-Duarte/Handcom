using AutoMapper;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;

namespace Handcom.Ioc.Configuration
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Topics, TopicsCreateRequestDto>().ReverseMap();
                config.CreateMap<Posts, PostsCreateRequestDto>().ReverseMap();
                config.CreateMap<Comments, CommentsCreateRequestDto>().ReverseMap();
                config.CreateMap<Comments, TopicsUpdateRequestDto>().ReverseMap();

            });
        }
    }
}
