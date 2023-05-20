using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;

public record GetCourseVisualizerQuery(Guid Id) : IRequest<Result<GetCourseVisualizerQueryResponse>>;