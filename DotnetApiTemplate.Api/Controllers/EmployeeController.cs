using DotnetApiTemplate.Api.Constants;
using DotnetApiTemplate.Api.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetApiTemplate.Api.Authorization;

namespace DotnetApiTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AppPolicies.DEFAULT)]
    public class EmployeeController : AppBaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, IConfiguration config, ILogger<EmployeeController> logger) : base(config, logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet(Name = "GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.Get();
            return OkDone(result);
        }

        [HttpGet("/{employeeId:int}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var result = await _employeeService.Get();
            return OkDone(result);
        }

        [HttpPost(Name = "CreateEmployee")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto emp)
        {
            var result = await _employeeService.CreateAsync(emp);
            if (result.EmployeeId > 0)
                return OkDone(result);
            else
                return InternalServerError();
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateDto emp)
        {
            var result = await _employeeService.UpdateEmployeeAsync(emp);
            return OkDone(result);
        }

        [HttpDelete(Name = "DeleteEmployee")]
        [Authorize(Policy = AppPolicies.ADMIN)]
        public async Task<IActionResult> DeleteEmployee([FromBody] int empId)
        {
            var isSuccess = await _employeeService.DeleteEmployeeAsync(empId);

            if(isSuccess)
                return OkDone(isSuccess);
            else
                return InternalServerError();
        }
    }
}
