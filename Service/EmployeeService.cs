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
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger,IMapper mapper)
        {
            _repository= repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employees = _repository.Employee.GetAllEmployees(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyid,Guid id,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyid,trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyid);

            var employee = _repository.Employee.GetEmployee(companyid, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyid,EmployeeForCreationDto employeeForCreationDto,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyid, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyid);

            var employeeEntity = _mapper.Map<Employee> (employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyid, employeeEntity);

            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public void DeleteEmployeeForCompany(Guid companyId,Guid id,bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges);

            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(employee);

            _repository.Save();
        }
    }
}
