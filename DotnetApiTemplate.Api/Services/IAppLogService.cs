using Application.Dtos;

namespace DotnetApiTemplate.Api.Services
{
    public interface IAppLogService
    {
        Task<List<ActivityLogDto>> GetActivityLogsAsync(SearchCriteria searchCriteria);
        Task<List<ErrorLogDto>> GetErrorLogsAsync(SearchCriteria searchCriteria);
    }
}
