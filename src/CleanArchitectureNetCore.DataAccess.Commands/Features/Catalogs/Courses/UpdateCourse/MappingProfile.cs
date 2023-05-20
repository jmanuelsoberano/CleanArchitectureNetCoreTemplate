using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;
using CleanArchitectureNetCore.Domain.Courses;

namespace CleanArchitectureNetCore.DataAccess.Commands.Features.Catalogs.Courses.UpdateCourse;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateCourseCommand, Course>()
            .ForMember(dest => dest.Modules, opt => opt.Ignore());
        CreateMap<ModuleDto, Module>()
            .ForMember(dest => dest.Clips, opt => opt.Ignore());
        CreateMap<ClipDto, Clip>();
    }
}