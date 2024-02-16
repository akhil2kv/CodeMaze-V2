using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
             await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCompany(Company compnay) => Create(compnay);

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
           await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteCompany(Company company) => Delete(company);
    }
}
