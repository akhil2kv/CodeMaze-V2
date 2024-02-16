using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(Guid id ,bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyid,Employee employee);
        void DeleteEmployee(Employee employee);      
    }
}
