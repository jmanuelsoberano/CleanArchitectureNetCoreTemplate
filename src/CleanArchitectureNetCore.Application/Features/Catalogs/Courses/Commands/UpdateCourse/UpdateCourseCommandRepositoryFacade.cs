using AutoMapper;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Courses;

namespace CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandRepositoryFacade : IUpdateCourseCommandRepositoryFacade
{
    private readonly IRepository<Course> _repository;
    private readonly IUpdateCourseCommandRepository _updateCourseCommandRepository;

    public UpdateCourseCommandRepositoryFacade(IRepository<Course> repository,
        IUpdateCourseCommandRepository updateCourseCommandRepository)
    {
        _repository = repository;
        _updateCourseCommandRepository = updateCourseCommandRepository;
    }

    public async Task<bool> ExistsCourseAsync(Guid id)
    {
        return await _repository.ExistByIdAsync(id);
    }

    public async Task UpdateCourse(UpdateCourseCommand request)
    {
        await _updateCourseCommandRepository.UpdateCourseAsync(request);
    }
}