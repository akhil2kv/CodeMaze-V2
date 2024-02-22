using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(Guid companyid, EmployeeParameters employeeParameters, bool trackChanges) =>
             await FindByCondition(e => e.CompanyId.Equals(companyid), trackChanges)
            .OrderBy(e => e.Name)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();

        public async Task<Employee> GetEmployeeAsync(Guid companyid, Guid id, bool trackChanges) =>
             await FindByCondition(e => e.CompanyId.Equals(companyid) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyid, Employee employee)
        {
            employee.CompanyId = companyid;
            Create(employee);
        }
        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
