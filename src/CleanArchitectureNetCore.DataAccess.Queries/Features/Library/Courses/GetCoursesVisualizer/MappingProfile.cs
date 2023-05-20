using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCoursesVisualizer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CourseWithTotalDurationModel, CourseModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.TotalDuration, opt => opt.MapFrom(src => src.TotalDuration));
    }
}