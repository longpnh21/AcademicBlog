using AutoMapper;
using System;

namespace Application.Mappers
{
    public class AcademicBlogMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<BlogMappingProfile>();
                cfg.AddProfile<TagMappingProfile>();
                cfg.AddProfile<CategoryMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
