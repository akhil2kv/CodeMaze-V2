using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
        EmployeeDto CreateEmployeeForCompany(Guid id,EmployeeForCreationDto employeeForCreationDto, bool trackChanges);
        void DeleteEmployeeForCompany(Guid companyId, Guid id,bool trackChanges);
        void UpdateEmployeeForCompany(Guid companyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges,bool empTrackChanges);
        (EmployeeForUpdateDto employeeTopatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid id, bool compChanges,bool empChanges);
        void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
    }
}
