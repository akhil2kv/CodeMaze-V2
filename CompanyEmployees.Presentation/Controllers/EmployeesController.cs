using CompanyEmployees.Presentation.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,[FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResult = await _service.EmployeeService.GetAllEmployeesAsync(companyId, employeeParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metadata));

            return Ok(pagedResult.employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployee(Guid companyId,Guid id)
        {
            var employee =  await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyid, [FromBody] EmployeeForCreationDto employeeForCreationDto)
        {
            var employeeToReturn =await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyid, employeeForCreationDto, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyid, id = employeeToReturn.id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async  Task<IActionResult> DeleteEmployeeForCompany(Guid companyid, Guid id)
        {
            await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyid, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId,Guid id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
        {
            await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId,id, employeeForUpdateDto, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdatedEmployeeForCompany(Guid companyId,Guid id,[FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if(patchDoc is null)
                return BadRequest("patchDoc object is null");

            var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.employeeToPatch);

            await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }
    }
}
