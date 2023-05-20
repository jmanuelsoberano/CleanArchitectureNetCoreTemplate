using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCourseVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Common;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCourseVisualizer;

public class GetCourseVisualizerQueryRepository : IGetCourseVisualizerQueryRepository
{
    private readonly IMapper _mapper;
    private readonly string _connectionString;

    public GetCourseVisualizerQueryRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("CleanArchitectureNetCoreConnectionString")!;
    }

    public async Task<GetCourseVisualizerQueryResponse> GetCourseAsync(GetCourseVisualizerQuery query)
    {
        var queryRaw = @"
            SELECT c.Id          AS CourseId,
                   c.Name        AS CourseName,
                   c.Description AS CourseDescription,
                   c.Level       AS CourseLevel,
                   m.Id          AS ModuleId,
                   m.Name        AS ModuleName,
                   m.Position    AS ModulePosition,
                   cl.Id         AS ClipId,
                   cl.Name       AS ClipName,
                   cl.Position   AS ClipPosition,
                   cl.Duration   AS ClipDuration,
                   cl.Url        AS ClipUrl
            FROM Courses c
                     LEFT JOIN Module m ON m.CourseId = c.Id
                     LEFT JOIN Clip cl ON cl.ModuleId = m.Id
            WHERE c.Id = @Id
            ORDER BY m.Position, cl.Position
        ";

        var list = await new SqlWrapper<CourseWithModulesAndClipsModel>(_connectionString)
            .SetQuery(queryRaw)
            .GetListAsync(new { Id = query.Id.ToString() });

        var result = list
            .GroupBy(x => new { x.CourseId, x.CourseName, x.CourseDescription, x.CourseLevel })
            .Select(curso => new CourseResponseModel(
                curso.Key.CourseId,
                curso.Key.CourseName,
                curso.Key.CourseDescription,
                curso.Key.CourseLevel,
                curso
                    .Where(w => w.ModuleId != Guid.Empty)
                    .GroupBy(m => new { m.ModuleId, m.ModuleName, m.ModulePosition })
                    .Select(modulo => new ModuleResponseModel(
                        modulo.Key.ModuleId,
                        modulo.Key.ModuleName,
                        modulo.Key.ModulePosition,
                        modulo
                            .Where(w => w.ClipId != Guid.Empty)
                            .Select(clip => new ClipResponseModel(
                                clip.ClipId,
                                clip.ClipName,
                                clip.ClipPosition,
                                clip.ClipDuration,
                                clip.ClipUrl))
                            .ToList()))
                    .ToList()))
            .Single();

        return _mapper.Map<GetCourseVisualizerQueryResponse>(result);
    }
}