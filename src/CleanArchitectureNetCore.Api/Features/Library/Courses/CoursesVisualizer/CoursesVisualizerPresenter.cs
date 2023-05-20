using AutoMapper;
using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Library.Courses.CoursesVisualizer;

[Route("api/library/[controller]")]
[Authorize]
[ApiController]
public class CoursesVisualizerPresenter : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CoursesVisualizerPresenter(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCoursesVisualizer([FromQuery] GetCoursesVisualizerQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok(_mapper.Map<CoursesVisualizerVm>(result.Value));
    }
}