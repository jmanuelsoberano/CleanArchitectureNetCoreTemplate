using AutoMapper;
using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Commands.UpdateCourse;
using CleanArchitectureNetCore.DataAccess.Commands.Shared;
using CleanArchitectureNetCore.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.DataAccess.Commands.Features.Catalogs.Courses.UpdateCourse;

public class UpdateCourseCommandRepository : IUpdateCourseCommandRepository
{
    private readonly IDatabaseContext _database;
    private readonly IRepository<Module> _moduleRepository;
    private readonly IRepository<Clip> _clipRepository;
    private readonly IMapper _mapper;

    public UpdateCourseCommandRepository(IDatabaseContext database,
        IRepository<Module> moduleRepository,
        IRepository<Clip> clipRepository,
        IMapper mapper)
    {
        _database = database;
        _moduleRepository = moduleRepository;
        _clipRepository = clipRepository;
        _mapper = mapper;
    }

    public async Task UpdateCourseAsync(UpdateCourseCommand request)
    {
        var courseToUpdate = await _database.Courses.Include(c => c.Modules)
            .ThenInclude(m => m.Clips)
            .SingleAsync(c => c.Id == request.Id);

        _mapper.Map(request, courseToUpdate);

        UpdateModules(courseToUpdate.Modules, request.Modules, request);
    }

    private void UpdateModules(IEnumerable<Module> currentModules, IEnumerable<ModuleDto> modules,
        UpdateCourseCommand requestCourse)
    {
        var modulesToRemove = currentModules.Where(m => modules.All(dto => dto.Id != m.Id)).ToList();
        if (modulesToRemove.Count > 0) _moduleRepository.DeleteRange(modulesToRemove);

        foreach (var moduleDto in modules)
        {
            var module = currentModules.FirstOrDefault(m => m.Id == moduleDto.Id);
            if (module == null)
            {
                var moduleToAdd = _mapper.Map<Module>(moduleDto);
                moduleToAdd.CourseId = requestCourse.Id;
                moduleToAdd.Clips = moduleDto.Clips.Select(s => _mapper.Map<Clip>(s)).ToList();
                _moduleRepository.Add(moduleToAdd);
            }
            else
            {
                _mapper.Map(moduleDto, module);

                UpdateClips(module.Clips, moduleDto.Clips, moduleDto);
            }
        }
    }

    private void UpdateClips(IEnumerable<Clip> currentClips, IEnumerable<ClipDto> clips, ModuleDto requestModule)
    {
        var clipsToRemove = currentClips.Where(c => clips.All(dto => dto.Id != c.Id)).ToList();
        if (clipsToRemove.Count > 0) _clipRepository.DeleteRange(clipsToRemove);

        foreach (var clipDto in clips)
        {
            var clip = currentClips.FirstOrDefault(c => c.Id == clipDto.Id);
            if (clip == null)
            {
                var clipToAdd = _mapper.Map<Clip>(clipDto);
                clipToAdd.ModuleId = requestModule.Id;
                _clipRepository.Add(clipToAdd);
            }
            else
            {
                _mapper.Map(clipDto, clip);
            }
        }
    }
}