using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {

            var companies = _repository.Company.GetAllCompanies(trackChanges);

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }

        public CompanyDto GetCompany(Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(id);

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var CompanyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(CompanyEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(CompanyEntity);

            return companyToReturn;
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids , bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var companyEntities = _repository.Company.GetByIds(ids, trackChanges);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return companiesToReturn;
        }

        public (IEnumerable<CompanyDto> companies ,string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if(companyCollection is null)
                throw new CompanyCollectionBadRequest();

            // mapping the company collection to company entities , because we need to add them to the database
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection); 

            // adding the company entities to the database 
            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }

            _repository.Save();

            // mapping the company entities to company data transfer objects , because we need to return them to the client
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            // creating a string of the ids of the companies that we created and added to the database
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            // returning the company data transfer objects and the ids string , 
            // and syntax  is returing multiple values from a method using a tuple 
            // for different ways to return multiple values from a method see : https://www.c-sharpcorner.com/article/returning-multiple-values-from-a-method-in-C-Sharp/
            return (companies: companyCollectionToReturn, ids: ids);

        }

        public void DeleteCompany(Guid companyId,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            _repository.Company.DeleteCompany(company);

            _repository.Save();
        }

    }
}
