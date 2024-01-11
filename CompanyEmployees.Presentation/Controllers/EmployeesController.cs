using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public  class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetAllEmployees(companyId, trackChanges: false);
            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployee(Guid companyId,Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyid, [FromBody] EmployeeForCreationDto employeeForCreationDto)
        {
            if(employeeForCreationDto is null)
                return BadRequest("EmployeeForCreationDto object is null");

            var employeeToReturn =_service.EmployeeService.CreateEmployeeForCompany(companyid,employeeForCreationDto, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyid, id = employeeToReturn.id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyid, Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyid, id, trackChanges: false);

            return NoContent();
        }
    }
}
