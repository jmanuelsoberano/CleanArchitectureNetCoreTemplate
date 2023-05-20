using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

namespace CleanArchitectureNetCore.Api.Features.Catalogs.Courses.CoursesVisualizer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetCoursesVisualizerQueryRespond, CoursesVisualizerVm>()
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
            .ForMember(dest => dest.Pagination, opt => opt.MapFrom(src => src.Pagination));

        CreateMap<CourseModel, CourseVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.QuantityOfModules, opt => opt.MapFrom(src => src.QuantityOfModules));
    }
}