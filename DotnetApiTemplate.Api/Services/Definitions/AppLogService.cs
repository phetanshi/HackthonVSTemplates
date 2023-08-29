using Application.Dtos;
using AutoMapper;
using DotnetApiTemplate.Data.Models;
using DotnetApiTemplate.Logging.Database.Models;
using DotnetApiTemplate.Logging.LogRepo;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;
using System.Linq;

namespace DotnetApiTemplate.Api.Services.Definitions
{
    public class AppLogService : ServiceBase, IAppLogService
    {
        private readonly ILogRepository logRepository;

        public AppLogService(ILogRepository logRepository, IRepository repository, ILogger<AppLogService> logger, IConfiguration config, IMapper mapper) : base(repository, logger, config, mapper)
        {
            this.logRepository = logRepository;
        }
        public async Task<List<ActivityLogDto>> GetActivityLogsAsync(SearchCriteria searchCriteria)
        {
            int page = searchCriteria.Page - 1;
            int row = searchCriteria.Rows;

            var activities = await logRepository.GetList<ActivityLog>()
                                            .Include(x => x.ActivtyType)
                                            .Where(x => string.IsNullOrEmpty(searchCriteria.SearchTerm)
                                                            || x.ActivityDesc.ToLower()
                                                                             .Contains(searchCriteria.SearchTerm.Trim().ToLower()))
                                            .Skip(page * row)
                                            .Take(row)
                                            .ToListAsync();

            var employeeIds = activities.Select(x => x.EmployeeId).Distinct().ToList();
            var empList = await Repository.GetList<Employee>(x => employeeIds.Contains(x.EmployeeId)).ToListAsync();

            var result = from ac in activities
                         join employee in empList on ac.EmployeeId equals employee.EmployeeId into empData
                         from emp in empData.DefaultIfEmpty()
                         select new ActivityLogDto
                         {
                             ActivityId = ac.ActivityId,
                             EmployeeId = emp?.EmployeeId ?? 0,
                             EmployeeName = emp?.ToString() ?? "Unknown",
                             ActivityType = ac.ActivtyType != null ? ac.ActivtyType.Type : null,
                             ActivityDesc = ac.ActivityDesc,
                             ActivityTimeStamp = ac.ActivityTimeStamp
                         };

            return result.ToList();
        }

        public async Task<List<ErrorLogDto>> GetErrorLogsAsync(SearchCriteria searchCriteria)
        {
            int page = searchCriteria.Page - 1;
            int row = searchCriteria.Rows;

            var errors = await logRepository.GetList<ErrorLog>()
                                            .Include(x => x.ErrorType)
                                            .Where(x => string.IsNullOrEmpty(searchCriteria.SearchTerm)
                                                            || x.ErrorMessage.ToLower()
                                                                             .Contains(searchCriteria.SearchTerm.Trim().ToLower()))
                                            .Skip(page * row)
                                            .Take(row)
                                            .ToListAsync();

            var employeeIds = errors.Select(x => x.EmployeeId).Distinct().ToList();
            var empList = await Repository.GetList<Employee>(x => employeeIds.Contains(x.EmployeeId)).ToListAsync();

            var result = from er in errors
                         join employee in empList on er.EmployeeId equals employee.EmployeeId into empData
                         from emp in empData.DefaultIfEmpty()
                         select new ErrorLogDto
                         {
                             ErrorId = er.ErrorId,
                             EmployeeId = emp?.EmployeeId ?? 0,
                             EmployeeName = emp?.ToString() ?? "Unknown",
                             ErrorType = er.ErrorType != null ? er.ErrorType.Type : null,
                             ErrorMessage = er.MethodName,
                             StackTrace = er.StackTrace,
                             ErrorTimeStamp = er.TimeStamp
                         };

            return result.ToList();
        }
    }
}
