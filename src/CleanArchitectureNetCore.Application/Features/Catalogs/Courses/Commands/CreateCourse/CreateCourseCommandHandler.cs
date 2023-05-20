using AutoMapper;
using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IRepository<Course> repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new CreateCourseCommandValidator(_repository).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        CreateCourse(request);

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

    private void CreateCourse(CreateCourseCommand request)
    {
        var course = _mapper.Map<Course>(request);
        _repository.Add(course);
    }
}