using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : IRequest<Result>;