using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCourseVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Common;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCourseVisualizer;

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
            WITH ModuleDurations AS (SELECT m.Id                                                        as ModuleId,
                                            COALESCE(SUM(DATEDIFF(SECOND, '00:00:00', cl.Duration)), 0) as ModuleDurationInSeconds
                                     FROM Module m
                                              LEFT JOIN Clip cl ON m.Id = cl.ModuleId
                                     GROUP BY m.Id),
                 CourseDurations AS (SELECT c.Id                                         as CourseId,
                                            COALESCE(SUM(md.ModuleDurationInSeconds), 0) as CourseDurationInSeconds
                                     FROM Courses c
                                              LEFT JOIN Module m ON c.Id = m.CourseId
                                              LEFT JOIN ModuleDurations md ON m.Id = md.ModuleId
                                     GROUP BY c.Id)

            SELECT c.Id                            CourseId,
                   c.Name                          CourseName,
                   c.Description                   CourseDescription,
                   c.Level                         CourseLevel,
                   m.id                            ModuleId,
                   m.name                          ModuleName,
                   m.Position                      ModulePosition,
                   cl.Id                           ClipId,
                   cl.Name                         ClipName,
                   cl.Position                     ClipPosition,
                   CONVERT(varchar, Duration, 108) ClipDuration,
                   cl.Url                          ClipUrl,
                   CONCAT(
                           FORMAT(COALESCE(md.ModuleDurationInSeconds, 0) / 3600, '00'), ':',
                           FORMAT((COALESCE(md.ModuleDurationInSeconds, 0) % 3600) / 60, '00'), ':',
                           FORMAT(COALESCE(md.ModuleDurationInSeconds, 0) % 60, '00')
                       ) as                        ModuleDuration,
                   CONCAT(
                           FORMAT(COALESCE(cd.CourseDurationInSeconds, 0) / 3600, '00'), ':',
                           FORMAT((COALESCE(cd.CourseDurationInSeconds, 0) % 3600) / 60, '00'), ':',
                           FORMAT(COALESCE(cd.CourseDurationInSeconds, 0) % 60, '00')
                       ) as                        CourseDuration
            FROM Courses c
                     LEFT JOIN CourseDurations cd ON c.Id = cd.CourseId
                     LEFT JOIN Module m ON c.Id = m.CourseId
                     LEFT JOIN ModuleDurations md ON m.Id = md.ModuleId
                     LEFT JOIN Clip cl ON m.Id = cl.ModuleId
            WHERE c.Id = @Id
            ORDER BY m.Position, cl.Position;
        ";

        var list = await new SqlWrapper<CourseWithModulesAndClipsModel>(_connectionString)
            .SetQuery(queryRaw)
            .GetListAsync(new { Id = query.Id.ToString() });

        var result = list
            .GroupBy(x => new { x.CourseId, x.CourseName, x.CourseDescription, x.CourseLevel, x.CourseDuration })
            .Select(curso => new CourseResponseModel(
                curso.Key.CourseId,
                curso.Key.CourseName,
                curso.Key.CourseDescription,
                curso.Key.CourseLevel,
                curso.Key.CourseDuration,
                curso
                    .Where(w => w.ModuleId != Guid.Empty)
                    .GroupBy(m => new { m.ModuleId, m.ModuleName, m.ModulePosition, m.ModuleDuration })
                    .Select(modulo => new ModuleResponseModel(
                        modulo.Key.ModuleId,
                        modulo.Key.ModuleName,
                        modulo.Key.ModulePosition,
                        modulo.Key.ModuleDuration,
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