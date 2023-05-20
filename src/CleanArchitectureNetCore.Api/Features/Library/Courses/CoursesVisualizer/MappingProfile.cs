using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

namespace CleanArchitectureNetCore.Api.Features.Library.Courses.CoursesVisualizer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetCoursesVisualizerQueryResponse, CoursesVisualizerVm>()
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));

        CreateMap<CourseModel, CourseVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.TotalDuration));
    }
}