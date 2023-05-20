using AutoMapper;
using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IRepository<Course> repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new DeleteCourseCommandValidator(_repository).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        var courseToDelete = await _repository.GetByIdAsync(request.Id);

        DeleteCourse(courseToDelete);

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    private void DeleteCourse(Course course)
    {
        _repository.Delete(course);
    }
}