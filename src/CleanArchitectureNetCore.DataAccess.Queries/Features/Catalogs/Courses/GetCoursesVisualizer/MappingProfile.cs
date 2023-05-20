using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCoursesVisualizer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CourseWithQuantityOfModulesModel, CourseModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.QuantityOfModules, opt => opt.MapFrom(src => src.QuantityOfModules));
    }
}