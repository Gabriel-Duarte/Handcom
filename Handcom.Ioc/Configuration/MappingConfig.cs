using AutoMapper;
using Handcom.Domain.Dto.Request;
using Handcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
