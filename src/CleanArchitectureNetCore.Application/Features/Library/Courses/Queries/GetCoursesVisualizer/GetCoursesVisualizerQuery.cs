using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;

public record GetCoursesVisualizerQuery() : IRequest<Result<GetCoursesVisualizerQueryResponse>>;