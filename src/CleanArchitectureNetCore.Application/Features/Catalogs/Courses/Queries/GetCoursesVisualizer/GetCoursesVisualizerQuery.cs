using CleanArchitectureNetCore.Application.Common.Pagination;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;

public record GetCoursesVisualizerQuery() : QueryParamsForListRequest,
    IRequest<Result<GetCoursesVisualizerQueryRespond>>;