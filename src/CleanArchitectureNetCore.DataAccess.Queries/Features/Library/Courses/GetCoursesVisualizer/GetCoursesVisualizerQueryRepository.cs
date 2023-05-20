using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Library.Courses.Queries.GetCoursesVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Common;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Library.Courses.GetCoursesVisualizer;

public class GetCoursesVisualizerQueryRepository : IGetCoursesVisualizerQueryRepository
{
    private readonly IMapper _mapper;
    private readonly string _connectionString;

    public GetCoursesVisualizerQueryRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("CleanArchitectureNetCoreConnectionString")!;
    }

    public async Task<GetCoursesVisualizerQueryResponse> GetCoursesAsync(GetCoursesVisualizerQuery query)
    {
        var list = await new SqlWrapper<CourseWithTotalDurationModel>(_connectionString)
            .SetQuery(@"
                SELECT c.Id, c.Name, c.Level,
                    CONCAT(
                        FORMAT(COALESCE(SUM(NULLIF(DATEDIFF(SECOND, '00:00:00', cl.Duration), 0)), 0) / 3600, '00'), ':',
                        FORMAT((COALESCE(SUM(NULLIF(DATEDIFF(SECOND, '00:00:00', cl.Duration), 0)), 0) % 3600) / 60, '00'), ':',
                        FORMAT(COALESCE(SUM(NULLIF(DATEDIFF(SECOND, '00:00:00', cl.Duration), 0)), 0) % 60, '00')
                    ) as TotalDuration
                FROM Courses c
                LEFT JOIN Module m ON c.Id = m.CourseId
                LEFT JOIN Clip cl ON m.Id = cl.ModuleId
                GROUP BY c.Id, c.Name, c.Level;
            ")
            .GetListAsync();

        return new GetCoursesVisualizerQueryResponse(_mapper.Map<IEnumerable<CourseModel>>(list));
    }
}