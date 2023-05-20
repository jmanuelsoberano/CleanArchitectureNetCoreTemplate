using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.CreateCourse;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.DeleteCourse;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Catalogs.Courses;

[Route("api/catalogs/courses/[controller]")]
[Authorize]
[ApiController]
public class CoursesController : BaseController
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok(command);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok(command);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCourse([FromQuery] DeleteCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok();
    }
}