using DotnetApiTemplate.Data.Models;
using Application.Dtos;

namespace DotnetApiTemplate.Api.Services
{
    public interface IEmployeeService
    {
        Task<bool> DeleteEmployeeAsync(int employeeId);
        Task<List<EmployeeReadDto>> Get();
        Task<EmployeeReadDto> Get(int employeeId);
        Task<EmployeeReadDto> Get(string loginUserId);
        Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto empDto);
        Task<EmployeeReadDto> UpdateEmployeeAsync(EmployeeUpdateDto empDto);
    }
}
