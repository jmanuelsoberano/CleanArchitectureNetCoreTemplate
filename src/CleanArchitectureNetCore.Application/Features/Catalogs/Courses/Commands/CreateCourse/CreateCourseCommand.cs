using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.CreateCourse;

public record CreateCourseCommand(Guid Id, string Name, string Description, string Level,
    IEnumerable<ModuleDto>? Modules) : IRequest<Result>;

public record ModuleDto(Guid Id, string Name, int Position, IEnumerable<ClipDto> Clips);

public record ClipDto(Guid Id, string Name, int Position, TimeSpan Duration, string Url);