using AutoMapper;
using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Library.Courses.CourseVisualizer;

[Route("api/library/[controller]")]
[Authorize]
[ApiController]
public class CourseVisualizerPresenter : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CourseVisualizerPresenter(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourseVisualizer([FromQuery] GetCourseVisualizerQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok(_mapper.Map<CourseVm>(result.Value));
    }
}