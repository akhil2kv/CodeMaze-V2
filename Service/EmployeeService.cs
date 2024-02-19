using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private async Task<Employee> GetEmployeeAndCheckIfItExists(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(employeeId);

            return employee;
        }

        private async Task<Company> GetCompanyForEmployeeAndCheckIfItExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            return company;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyId, trackChanges);

            var employees =await  _repository.Employee.GetAllEmployeesAsync(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyId, trackChanges);

            var employee = await GetEmployeeAndCheckIfItExists(companyId, id, trackChanges);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyid,EmployeeForCreationDto employeeForCreationDto,bool trackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyid, trackChanges);

            var employeeEntity = _mapper.Map<Employee> (employeeForCreationDto);

            _repository.Employee.CreateEmployeeForCompany(companyid, employeeEntity);

            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId,Guid id,bool trackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyId, trackChanges);

            var employee = await GetEmployeeAndCheckIfItExists(companyId, id, trackChanges);

            _repository.Employee.DeleteEmployee(employee);

            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId,Guid id,EmployeeForUpdateDto employeeForUpdateDto,bool compTrackChanges,bool empTrackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyId, compTrackChanges);

            var employee = await GetEmployeeAndCheckIfItExists(companyId, id, empTrackChanges);

            _mapper.Map(employeeForUpdateDto, employee);

            await _repository.SaveAsync();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
    (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var company = await GetCompanyForEmployeeAndCheckIfItExists(companyId, compTrackChanges);

            var employeeEntity = await GetEmployeeAndCheckIfItExists(companyId, id, empTrackChanges);

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
