using System.Text.Json;
using CleanArchitectureNetCore.Application.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Common;

public class BaseController : ControllerBase
{
    protected new IActionResult Ok()
    {
        return base.Ok(Envelope.Ok());
    }

    protected IActionResult Ok<T>(T result)
    {
        return base.Ok(Envelope.Ok(result));
    }

    // protected FileContentResult File(IFormat format)
    // {
    //     Response.Headers.Add("content-disposition",
    //         $"attachment; filename={Guid.NewGuid()}{format.GetExtension()}");
    //     return base.File(format.GetContent(), format.GetContentType());
    // }

    protected IActionResult Error(string errorMessage)
    {
        return BadRequest(Envelope.Error(errorMessage));
    }

    protected void AddHeaderPagination(IPagedList pagedList)
    {
        var paginationMetadata = new
        {
            totalCount = pagedList.TotalCount,
            pageSize = pagedList.PageSize,
            currentPage = pagedList.CurrentPage,
            totalPages = pagedList.TotalPages,
            hasPrevious = pagedList.HasPrevious,
            hasNext = pagedList.HasNext
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
    }
}