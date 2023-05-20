using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUpdateCourseCommandRepositoryFacade _repository;

    public UpdateCourseCommandHandler(IUnitOfWork unitOfWork, IUpdateCourseCommandRepositoryFacade repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new UpdateCourseCommandValidator(_repository).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        await _repository.UpdateCourse(request);

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}