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

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employees =await  _repository.Employee.GetAllEmployeesAsync(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }
        public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee =await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyid,EmployeeForCreationDto employeeForCreationDto,bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyid, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyid);

            var employeeEntity = _mapper.Map<Employee> (employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyid, employeeEntity);

            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId,Guid id,bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);

            var employee =await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(employee);

            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto,bool compTrackChanges,bool empTrackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employee =await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdateDto, employee);

            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
    (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(companyId);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch,Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);

            await _repository.SaveAsync();
        }
    }
}
