using AutoMapper;
using CleanArchitectureNetCore.Application.Features.Catalogs.Courses.Queries.GetCoursesVisualizer;
using CleanArchitectureNetCore.DataAccess.Queries.Common;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureNetCore.DataAccess.Queries.Features.Catalogs.Courses.GetCoursesVisualizer;

public class GetCoursesVisualizerQueryRepository : IGetCoursesVisualizerQueryRepository
{
    private readonly IMapper _mapper;
    private readonly string _connectionString;

    public GetCoursesVisualizerQueryRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("CleanArchitectureNetCoreConnectionString")!;
    }

    public async Task<GetCoursesVisualizerQueryRespond> GetCoursesVisualizerAsync(GetCoursesVisualizerQuery query)
    {
        var list = await new SqlWrapper<CourseWithQuantityOfModulesModel>(_connectionString)
            .SetQuery(@"
                SELECT *
                FROM (SELECT C.Id, C.Name, C.Description, C.Level, COUNT(M.Id) QuantityOfModules
                      FROM Courses C
                               LEFT JOIN Module M ON C.Id = M.CourseId
                      GROUP BY C.Id, C.Name, C.Description, C.Level) AS RESULT
                WHERE RESULT.Id like '%' + @Search + '%'
                   OR RESULT.Name like '%' + @Search + '%'
                   OR RESULT.Description like '%' + @Search + '%'
                   OR RESULT.Level LIKE '%' + @Search + '%'
                   OR RESULT.QuantityOfModules LIKE '%' + @Search + '%'
            ")
            .SetOrderBy(query.OrderBy)
            .GetListAsync(query, new { query.Search });

        return new GetCoursesVisualizerQueryRespond(_mapper.Map<IEnumerable<CourseModel>>(list), list);
    }
}