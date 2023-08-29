using AutoMapper;
using DotnetApiTemplate.Data.Models;
using Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;

namespace DotnetApiTemplate.Api.Services.Definitions
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        public EmployeeService(IRepository repository, ILogger<EmployeeService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }

        public async Task<List<EmployeeReadDto>> Get()
        {
            var result = await Repository.GetList<Employee>().ToListAsync();
            return Mapper.Map<List<EmployeeReadDto>>(result);
        }
        public async Task<EmployeeReadDto> Get(int employeeId)
        {
            var result = await Repository.GetSingleAsync<Employee>(employeeId);
            return Mapper.Map<EmployeeReadDto>(result);
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto empDto)
        {
            Employee emp = new Employee();
            Mapper.Map(empDto, emp);
            emp.SetDefaultsForAuditFields(GetLoginUserId());
            await Repository.CreateAsync(emp);
            return Mapper.Map<EmployeeReadDto>(emp);
        }

        public async Task<EmployeeReadDto> UpdateEmployeeAsync(EmployeeUpdateDto empDto)
        {
            Employee emp = await Repository.GetSingleAsync<Employee>(empDto.EmployeeId);
            Mapper.Map(empDto, emp);
            emp.SetDefaultsForAuditFields(GetLoginUserId());
            await Repository.UpdateAsync(emp);
            return Mapper.Map<EmployeeReadDto>(emp);
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            await Repository.DeleteAsync<Employee>(employeeId);
            return true;
        }
    }
}
