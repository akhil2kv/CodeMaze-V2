using Contracts;
using Entities.Models;
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
        public IEnumerable<Employee> GetAllEmployees(Guid companyid, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyid), trackChanges)
            .OrderBy(e => e.Name)
            .ToList();

        public Employee GetEmployee(Guid companyid, Guid id, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyid) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

    }
}
